package sl.hr_client.main.download;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.widget.LinearLayoutManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.greenrobot.eventbus.EventBus;
import org.greenrobot.eventbus.Subscribe;
import org.greenrobot.eventbus.ThreadMode;

import java.util.ArrayList;
import java.util.List;

import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.ui.recyclerview.ProgressStyle;
import sl.base.ui.recyclerview.XRecyclerView;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.ContentFragment;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.DownloadBean;
import sl.hr_client.data.bean.LinkBean;
import sl.hr_client.data.bean.MemberBean;
import sl.hr_client.data.bean.list.DownloadListBean;
import sl.hr_client.data.GsonUtils;
import sl.hr_client.data.bean.list.LinkListBean;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.main.link.LinkAdapter;
import sl.hr_client.net.download.DownloadPresenter;
import sl.hr_client.net.download.DownloadView;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/4/12.
 */

public class DownloadFragment extends ContentFragment implements DownloadView {
    public static final String DOWNLOADS = "DOWNLOADS";

    private View downloadView;

    private Context ctx;
    private XRecyclerView xrvDownload;

    private DownloadPresenter downloadPresenter;
    private DownloadAdapter downloadAdapter;
    private List<DownloadBean> downloadsList = new ArrayList<>();

    private int pageIndex = 1;
    private int pageSize = 10;

    //加载处理
    private RelativeLayout rlLoading; //加载区域
    private ImageView ivLoadFailed; //加载失败
    private AVLoadingIndicatorView vLoading;//加载中
    private TextView tvLoadState; //加载文字
    private boolean loadedLastPage = false;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        downloadView = inflater.inflate(R.layout.fragment_download, container, false);

        ctx = downloadView.getContext();

        //注册EventBus
        if (!EventBus.getDefault().isRegistered(this)) {
            EventBus.getDefault().register(this);
        }

        return downloadView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        downloadPresenter = new DownloadPresenter(this);
        xrvDownload = (XRecyclerView) view.findViewById(R.id.xrv_download); //把初始化View分开写

        rlLoading = (RelativeLayout) view.findViewById(R.id.rl_loading);
        ivLoadFailed = (ImageView) view.findViewById(R.id.iv_load_failed);
        vLoading = (AVLoadingIndicatorView) view.findViewById(R.id.v_loading);
        tvLoadState = (TextView) view.findViewById(R.id.tv_load_state);

        //点击失败重新加载
        ivLoadFailed.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                funcGetData();
            }
        });

        //设置布局，不然不显示
        LinearLayoutManager mLayoutManager = new LinearLayoutManager(ctx);
        mLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        xrvDownload.setLayoutManager(mLayoutManager);

        xrvDownload.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvDownload.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvDownload.setArrowImageView(R.mipmap.iconfont_downgrey);

        funcGetData();

        xrvDownload.setLoadingListener(new XRecyclerView.LoadingListener() {
            @Override
            public void onRefresh() {
                funcGetData();
            }

            @Override
            public void onLoadMore() {
                funcLoadMoreData();
            }
        });
    }

    private void funcGetData() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            pageIndex = 1;
            loadedLastPage = false;
            downloadPresenter.getPDownload(ctx, pageIndex, pageSize, null);
        } else {
            List<DownloadBean> cache = DataUtils.getAllDownloadsCache();
            if (cache == null || cache.size() == 0) {
                showLoadingFailed();
            } else {
                downloadsList = cache;
                funcSetList();
                hideLoading();
            }
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private void funcSetList() {
        downloadAdapter = new DownloadAdapter(ctx, downloadsList);
        xrvDownload.setAdapter(downloadAdapter);
        xrvDownload.reset();
    }

    private void funcLoadMoreData() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            pageIndex = pageIndex + 1;
            downloadPresenter.loadMorePDownload(ctx, pageIndex, pageSize, null);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }


    @Override
    public void updateDownloadsView(String str) {
//        UtilsLog.logE(UtilsLog.getSte(), str);
        DownloadListBean tempBean = GsonUtils.parseDownloadList(str);

        if (tempBean != null) {
            downloadsList = tempBean.getResultList();
            funcSetList();
            DataUtils.addDownloads(downloadsList);
        }
    }

    @Override
    public void loadMoreDownloadsView(String str) {
        DownloadListBean moreBean = GsonUtils.parseDownloadList(str);
        if (moreBean != null) {
            if (moreBean.isLastPage() != true || loadedLastPage != true) {
                List<DownloadBean> moreList = moreBean.getResultList();
                downloadsList.addAll(moreList);
                downloadAdapter.notifyDataSetChanged();
                xrvDownload.loadMoreComplete();
                loadedLastPage = true;
            } else {
                xrvDownload.setNoMore(true);
            }

        }
    }

    @Override
    public void showLoading() {
        if (downloadsList.size() == 0) {
            rlLoading.setVisibility(View.VISIBLE);
            ivLoadFailed.setVisibility(View.GONE);
            vLoading.setVisibility(View.VISIBLE);
            tvLoadState.setText(ctx.getString(R.string.loading));
        }
    }

    @Override
    public void showLoadingFailed() {
        if (downloadsList.size() == 0) {
            rlLoading.setVisibility(View.VISIBLE);
            ivLoadFailed.setVisibility(View.VISIBLE);
            vLoading.setVisibility(View.GONE);
            tvLoadState.setText(ctx.getString(R.string.load_failed));
        }
    }

    @Override
    public void hideLoading() {
        rlLoading.setVisibility(View.GONE);
    }

    @Override
    public void showError(String msg) {
//        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
        showLoadingFailed(); //自行执行
        xrvDownload.reset();
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    public void onEventDownloadSucceed(TransferEvent event) {
        if (event.getTargetTag().equals(TransDefine.EVENT_DOWNLOAD_SUCCEED)) {
            String downloadID = event.getMsg() == null ? ctx.getString(R.string.null_value) : event.getMsg();
            downloadPresenter.updatePDownloadNum(ctx, downloadID);
        }
    }


    @Override
    public void onDestroy() {
        super.onDestroy();
        EventBus.getDefault().unregister(this);
    }

}

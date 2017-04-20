package sl.hr_client.main.download.fragment;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.widget.LinearLayoutManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.sl.lib.ui.sidemenu.interfaces.ScreenShotAble;

import java.util.ArrayList;
import java.util.List;

import sl.base.ui.recyclerview.ProgressStyle;
import sl.base.ui.recyclerview.XRecyclerView;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.ContentFragment;
import sl.hr_client.data.bean.DownloadBean;
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.data.bean.list.DownloadListBean;
import sl.hr_client.data.bean.list.NewsListBean;
import sl.hr_client.data.parse.GsonUtils;
import sl.hr_client.main.download.adapter.DownloadAdapter;
import sl.hr_client.main.news.adapter.NewsAdapter;
import sl.hr_client.net.download.DownloadPresenter;
import sl.hr_client.net.download.DownloadView;

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

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        downloadView = inflater.inflate(R.layout.fragment_download, container, false);

        ctx = downloadView.getContext();

        return downloadView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        downloadPresenter = new DownloadPresenter(this);

        xrvDownload = (XRecyclerView) view.findViewById(R.id.xrv_download); //把初始化View分开写

        //设置布局，不然不显示
        LinearLayoutManager mLayoutManager = new LinearLayoutManager(ctx);
        mLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        xrvDownload.setLayoutManager(mLayoutManager);

        xrvDownload.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvDownload.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvDownload.setArrowImageView(R.mipmap.iconfont_downgrey);

        funcGetData(pageIndex, pageSize);

        xrvDownload.setLoadingListener(new XRecyclerView.LoadingListener() {
            @Override
            public void onRefresh() {
                pageIndex = 1;
                funcGetData(pageIndex, pageSize);
            }

            @Override
            public void onLoadMore() {
                pageIndex = pageIndex++;
                funcGetData(pageIndex, pageSize);
            }
        });
    }

    private void funcGetData(int curIndex, int curSize) {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            downloadPresenter.getPDownload(ctx, curIndex, curSize, null);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
            if (pageIndex != 1) { //复位下拉的Index
                pageIndex--;
            }
        }
    }


    @Override
    public void updateDownloadsView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        DownloadListBean tempBean = GsonUtils.parseDownloadList(str);
        downloadsList = tempBean.getResultList();
        downloadAdapter = new DownloadAdapter(ctx,downloadsList);
        xrvDownload.setAdapter(downloadAdapter);
        xrvDownload.reset();
    }

    @Override
    public void showLoading() {

    }

    @Override
    public void showLoadingFailed() {

    }

    @Override
    public void hideLoading() {

    }

    @Override
    public void showError(String msg) {
        UtilsLog.logE(UtilsLog.getSte(), msg);
    }
}

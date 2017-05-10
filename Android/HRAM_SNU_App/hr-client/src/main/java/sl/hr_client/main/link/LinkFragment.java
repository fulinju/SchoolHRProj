package sl.hr_client.main.link;

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
import sl.hr_client.data.bean.LinkBean;
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.data.bean.list.LinkListBean;
import sl.hr_client.data.GsonUtils;
import sl.hr_client.main.news.adapter.NewsAdapter;
import sl.hr_client.net.link.LinkPresenter;
import sl.hr_client.net.link.LinkView;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/4/22.
 */

public class LinkFragment extends ContentFragment implements LinkView {
    public static final String LINKS = "Links";

    private Context ctx;

    private View linksView;

    private LinkPresenter linksPresenter;

    private XRecyclerView xrvLinks;
    private LinkAdapter linkAdapter;
    private List<LinkBean> linksList = new ArrayList<>();

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
        linksView = inflater.inflate(R.layout.fragment_link, container, false);
        ctx = linksView.getContext();
        return linksView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        linksPresenter = new LinkPresenter(this);
        xrvLinks = (XRecyclerView) view.findViewById(R.id.xrv_lins); //把初始化View分开写

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
        xrvLinks.setLayoutManager(mLayoutManager);

        xrvLinks.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvLinks.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvLinks.setArrowImageView(R.mipmap.iconfont_downgrey);

        funcGetData();

        xrvLinks.setLoadingListener(new XRecyclerView.LoadingListener() {
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
            linksPresenter.getPLink(ctx, pageIndex, pageSize);
        } else {
            List<LinkBean> cache = DataUtils.getAllLinksCache();
            if (cache == null || cache.size() == 0) {
                showLoadingFailed();
            } else {
                linksList = cache;
                funcSetList();
                hideLoading();
            }
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private void funcSetList() {
        linkAdapter = new LinkAdapter(ctx, getFragmentManager(), linksList);
        xrvLinks.setAdapter(linkAdapter);
        xrvLinks.reset();
    }

    private void funcLoadMoreData() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            pageIndex = pageIndex + 1;
            linksPresenter.loadMorePLink(ctx, pageIndex, pageSize);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    @Override
    public void updateLinksView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        LinkListBean tempBean = GsonUtils.parseLinkList(str);
        if (tempBean != null) {
            linksList = tempBean.getResultList();
            funcSetList();
            DataUtils.addLinks(linksList);
        }
    }

    @Override
    public void updateLoadMoreLinksView(String str) {
        LinkListBean moreBean = GsonUtils.parseLinkList(str);
        if (moreBean != null) {
            if (moreBean.isLastPage() != true || loadedLastPage != true) {
                List<LinkBean> moreList = moreBean.getResultList();
                linksList.addAll(moreList);
                linkAdapter.notifyDataSetChanged();
                xrvLinks.loadMoreComplete();
                loadedLastPage = true;
            } else {
                xrvLinks.setNoMore(true);
            }

        }
    }

    @Override
    public void showLoading() {
        if (linksList.size() == 0) {
            rlLoading.setVisibility(View.VISIBLE);
            ivLoadFailed.setVisibility(View.GONE);
            vLoading.setVisibility(View.VISIBLE);
            tvLoadState.setText(ctx.getString(R.string.loading));
        }
    }

    @Override
    public void showLoadingFailed() {
        if (linksList.size() == 0) {
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
        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
        hideLoading(); //自行执行
        xrvLinks.reset();
    }
}

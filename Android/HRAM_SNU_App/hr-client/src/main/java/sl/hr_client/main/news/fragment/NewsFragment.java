package sl.hr_client.main.news.fragment;

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
import sl.hr_client.data.bean.list.NewsListBean;
import sl.hr_client.data.GsonUtils;
import sl.hr_client.main.news.adapter.NewsAdapter;
import sl.hr_client.net.news.NewsPresenter;
import sl.hr_client.net.news.NewsView;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/4/6.
 */

public class NewsFragment extends ContentFragment implements NewsView {
    public static final String NEWS = "News";

    private Context ctx;

    private View newsView;

    private NewsPresenter newsPresenter;

    private XRecyclerView xrvNews;
    private NewsAdapter newsAdapter;
    private List<NewsBean> newsList = new ArrayList<>();

    private int pageIndex = 1;
    private int pageSize = 10;

    //加载处理
    private RelativeLayout rlLoading; //加载区域
    private ImageView ivLoadFailed; //加载失败
    private AVLoadingIndicatorView vLoading;//加载中
    private TextView tvLoadState; //加载文字
    private boolean loadedLastPage = false;

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        newsPresenter = new NewsPresenter(this);

        xrvNews = (XRecyclerView) view.findViewById(R.id.xrv_news); //把初始化View分开写

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
        xrvNews.setLayoutManager(mLayoutManager);

        xrvNews.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvNews.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvNews.setArrowImageView(R.mipmap.iconfont_downgrey);
        funcGetData();

        xrvNews.setLoadingListener(new XRecyclerView.LoadingListener() {
            @Override
            public void onRefresh() {
                funcGetData();
            }

            @Override
            public void onLoadMore() {
                funcLoadMore();
            }
        });

    }

    private void funcGetData() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            pageIndex = 1;
            loadedLastPage = false;
            newsPresenter.getPNews(ctx, pageIndex, pageSize);
        } else {
            List<NewsBean> cache = DataUtils.getAllNewsCache();
            if (cache == null || cache.size() == 0) {
                showLoadingFailed();
            } else {
                newsList = cache;
                funcSetList();
                hideLoading();
            }
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private void funcLoadMore() {
        pageIndex = pageIndex + 1;
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            newsPresenter.loadMorePNews(ctx, pageIndex, pageSize);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        newsView = inflater.inflate(R.layout.fragment_news, container, false);

        ctx = newsView.getContext();
        return newsView;
    }

    @Override
    public void updateNewsView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        NewsListBean tempBean = GsonUtils.parseNewsList(str);
        if (tempBean != null) {
            newsList = tempBean.getResultList();

            DataUtils.addNews(newsList);
            funcSetList();
        }
    }

    private void funcSetList() {
        newsAdapter = new NewsAdapter(ctx, getFragmentManager(), newsList);
        xrvNews.setAdapter(newsAdapter);
//        xrvNews.refreshComplete();
        xrvNews.reset();
    }

    @Override
    public void loadMoreNewsView(String str) {
        NewsListBean moreBean = GsonUtils.parseNewsList(str);
        if (moreBean != null) {
            if (moreBean.isLastPage() != true || loadedLastPage != true) {
                List<NewsBean> moreList = moreBean.getResultList();
                newsList.addAll(moreList);
                newsAdapter.notifyDataSetChanged();
                xrvNews.loadMoreComplete();
                loadedLastPage = true;
            } else {
                xrvNews.setNoMore(true);
            }
        }
    }


    @Override
    public void showLoading() {
        if (newsList.size() == 0) {
            rlLoading.setVisibility(View.VISIBLE);
            ivLoadFailed.setVisibility(View.GONE);
            vLoading.setVisibility(View.VISIBLE);
            tvLoadState.setText(ctx.getString(R.string.loading));
        }
    }

    @Override
    public void showLoadingFailed() {
        if (newsList.size() == 0) {
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
        showLoadingFailed(); //自行执行
        xrvNews.reset();
    }
}

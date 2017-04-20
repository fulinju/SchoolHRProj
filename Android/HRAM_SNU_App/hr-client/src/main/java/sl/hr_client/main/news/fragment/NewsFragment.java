package sl.hr_client.main.news.fragment;

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
import sl.hr_client.base.BaseFragment;
import sl.hr_client.base.ContentFragment;
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.data.bean.list.NewsListBean;
import sl.hr_client.data.parse.GsonUtils;
import sl.hr_client.main.news.adapter.NewsAdapter;
import sl.hr_client.net.news.NewsPresenter;
import sl.hr_client.net.news.NewsView;

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


    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        newsPresenter = new NewsPresenter(this);

        xrvNews = (XRecyclerView) view.findViewById(R.id.xrv_news); //把初始化View分开写

        //设置布局，不然不显示
        LinearLayoutManager mLayoutManager = new LinearLayoutManager(ctx);
        mLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        xrvNews.setLayoutManager(mLayoutManager);

        xrvNews.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvNews.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvNews.setArrowImageView(R.mipmap.iconfont_downgrey);

        funcGetData(pageIndex, pageSize);

        xrvNews.setLoadingListener(new XRecyclerView.LoadingListener() {
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
            newsPresenter.getPNews(ctx, curIndex, curSize);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
            if (pageIndex != 1) { //复位下拉的Index
                pageIndex--;
            }
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
        newsList = tempBean.getResultList();
        newsAdapter = new NewsAdapter(ctx, getFragmentManager(), newsList);
        xrvNews.setAdapter(newsAdapter);
        xrvNews.reset();
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

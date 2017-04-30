package sl.hr_client.main.link;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.widget.LinearLayoutManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.ArrayList;
import java.util.List;

import sl.base.ui.recyclerview.ProgressStyle;
import sl.base.ui.recyclerview.XRecyclerView;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.ContentFragment;
import sl.hr_client.data.bean.LinkBean;
import sl.hr_client.data.bean.list.LinkListBean;
import sl.hr_client.data.parse.GsonUtils;
import sl.hr_client.net.link.LinkPresenter;
import sl.hr_client.net.link.LinkView;

/**
 * Created by Administrator on 2017/4/22.
 */

public class LinkFragment extends ContentFragment implements LinkView{
    public static final String LINKS = "Links";

    private Context ctx;

    private View linksView;

    private LinkPresenter linksPresenter;

    private XRecyclerView xrvLinks;
    private LinkAdapter linkAdapter;
    private List<LinkBean> linksList = new ArrayList<>();

    private int pageIndex = 1;
    private int pageSize = 10;

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

        //设置布局，不然不显示
        LinearLayoutManager mLayoutManager = new LinearLayoutManager(ctx);
        mLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        xrvLinks.setLayoutManager(mLayoutManager);

        xrvLinks.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvLinks.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvLinks.setArrowImageView(R.mipmap.iconfont_downgrey);

        funcGetData(pageIndex, pageSize);

        xrvLinks.setLoadingListener(new XRecyclerView.LoadingListener() {
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
            linksPresenter.getPLink(ctx, curIndex, curSize);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
            if (pageIndex != 1) { //复位下拉的Index
                pageIndex--;
            }
        }
    }

    @Override
    public void updateLinksView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        LinkListBean tempBean = GsonUtils.parseLinkList(str);
        linksList = tempBean.getResultList();
        linkAdapter = new LinkAdapter(ctx, getFragmentManager(), linksList);
        xrvLinks.setAdapter(linkAdapter);
        xrvLinks.reset();
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

    }
}

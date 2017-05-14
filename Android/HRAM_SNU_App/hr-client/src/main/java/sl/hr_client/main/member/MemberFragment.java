package sl.hr_client.main.member;

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
import sl.hr_client.data.bean.MemberBean;
import sl.hr_client.data.bean.list.LinkListBean;
import sl.hr_client.data.bean.list.MemberListBean;
import sl.hr_client.data.GsonUtils;
import sl.hr_client.main.link.LinkAdapter;
import sl.hr_client.net.member.MemberPresenter;
import sl.hr_client.net.member.MemberView;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/4/24.
 */

public class MemberFragment extends ContentFragment implements MemberView {
    public static final String MEMBERS = "Members";

    private Context ctx;

    private View membersView;

    private MemberPresenter memberPresenter;

    private XRecyclerView xrvMember;
    private MemberAdapter memberAdapter;
    private List<MemberBean> membersList = new ArrayList<>();

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
        membersView = inflater.inflate(R.layout.fragment_member, container, false);
        ctx = membersView.getContext();
        return membersView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        memberPresenter = new MemberPresenter(this);
        xrvMember = (XRecyclerView) view.findViewById(R.id.xrv_members); //把初始化View分开写

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
        xrvMember.setLayoutManager(mLayoutManager);

        xrvMember.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvMember.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvMember.setArrowImageView(R.mipmap.iconfont_downgrey);

        funcGetData();

        xrvMember.setLoadingListener(new XRecyclerView.LoadingListener() {
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
            memberPresenter.getPMember(ctx, pageIndex, pageSize);
        } else {
            List<MemberBean> cache = DataUtils.getAllMembersCache();
            if (cache == null || cache.size() == 0) {
                showLoadingFailed();
            } else {
                membersList = cache;
                funcSetList();
                hideLoading();
            }
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private void funcSetList() {
        memberAdapter = new MemberAdapter(ctx, getFragmentManager(), membersList);
        xrvMember.setAdapter(memberAdapter);
        xrvMember.reset();
    }

    private void funcLoadMoreData() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            pageIndex = pageIndex + 1;
            memberPresenter.loadMorePMember(ctx, pageIndex, pageSize);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    @Override
    public void updateMembersView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        MemberListBean tempBean = GsonUtils.parseMemberList(str);
        if (tempBean != null) {
            membersList = tempBean.getResultList();
            funcSetList();
            DataUtils.addMembers(membersList);
        }
    }

    @Override
    public void loadMoreMembersView(String str) {
        MemberListBean moreBean = GsonUtils.parseMemberList(str);
        if (moreBean != null) {
            if (moreBean.isLastPage() != true || loadedLastPage != true) {
                List<MemberBean> moreList = moreBean.getResultList();
                membersList.addAll(moreList);
                memberAdapter.notifyDataSetChanged();
                xrvMember.loadMoreComplete();
                loadedLastPage = true;
            } else {
                xrvMember.setNoMore(true);
            }

        }
    }

    @Override
    public void showLoading() {
        if (membersList.size() == 0) {
            rlLoading.setVisibility(View.VISIBLE);
            ivLoadFailed.setVisibility(View.GONE);
            vLoading.setVisibility(View.VISIBLE);
            tvLoadState.setText(ctx.getString(R.string.loading));
        }
    }

    @Override
    public void showLoadingFailed() {
        if (membersList.size() == 0) {
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
        xrvMember.reset();
    }
}

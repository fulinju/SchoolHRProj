package sl.hr_client.main.member;

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
import sl.hr_client.data.bean.MemberBean;
import sl.hr_client.data.bean.list.MemberListBean;
import sl.hr_client.data.parse.GsonUtils;
import sl.hr_client.net.member.MemberPresenter;
import sl.hr_client.net.member.MemberView;

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

        //设置布局，不然不显示
        LinearLayoutManager mLayoutManager = new LinearLayoutManager(ctx);
        mLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        xrvMember.setLayoutManager(mLayoutManager);

        xrvMember.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
        xrvMember.setLoadingMoreProgressStyle(ProgressStyle.BallClipRotateMultiple);
        xrvMember.setArrowImageView(R.mipmap.iconfont_downgrey);

        funcGetData(pageIndex, pageSize);

        xrvMember.setLoadingListener(new XRecyclerView.LoadingListener() {
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
            memberPresenter.getPMember(ctx, curIndex, curSize);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
            if (pageIndex != 1) { //复位下拉的Index
                pageIndex--;
            }
        }
    }

    @Override
    public void updateMembersView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        MemberListBean tempBean = GsonUtils.parseMemberList(str);
        membersList = tempBean.getResultList();
        memberAdapter = new MemberAdapter(ctx, getFragmentManager(), membersList);
        xrvMember.setAdapter(memberAdapter);
        xrvMember.reset();
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

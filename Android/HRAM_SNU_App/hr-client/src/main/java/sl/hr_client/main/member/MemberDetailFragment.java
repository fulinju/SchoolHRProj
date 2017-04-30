package sl.hr_client.main.member;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;

import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.data.bean.MemberBean;
import sl.hr_client.data.parse.GsonUtils;
import sl.hr_client.main.webview.WebViewFragment;
import sl.hr_client.net.member.detail.MemberDetailPresenter;
import sl.hr_client.net.member.detail.MemberDetailView;
import sl.hr_client.utils.constant.TransDefine;

/**
 * Created by Administrator on 2017/4/24.
 */

public class MemberDetailFragment extends BaseFragment implements View.OnClickListener, MemberDetailView {
    public static final String MEMBER_DETAIL = "MEMBER_DETAIL";

    public String memberID;

    private View newsDetailView;
    private Context ctx;

    private MemberDetailPresenter memberDetailPresenter;

    private TextView tvTitle;
    private TextView tvClose;

    private ImageView ivMember;
    private TextView tvMemberName;
    private TextView tvMemberType;
    private TextView tvMemberSummary;
    private TextView tvMemberOrganizationCode;
    private TextView tvMemberAddress;
    private TextView tvMemberCorporateName;
    private TextView tvMemberContacts;
    private TextView tvMemberContactsPhone;
    private Button btnVisitURL;

    private MemberBean member;

    private String memberURL;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        newsDetailView = inflater.inflate(R.layout.fragment_member_detail, container, false);

        ctx = newsDetailView.getContext();
        memberDetailPresenter = new MemberDetailPresenter(this);

        funcGetIntent();
        initView();
        return newsDetailView;
    }

    private void funcGetIntent() {
        memberID = getArguments().getString(TransDefine.Bundle_Member_ID);
        if (memberID == null) {
            UtilsToast.showToast(ctx, getString(R.string.cant_get_memberId));
            funcBack();
        }
    }

    private void initView() {
        tvTitle = (TextView) newsDetailView.findViewById(R.id.tv_head);
        tvClose = (TextView) newsDetailView.findViewById(R.id.tv_head_right);

        ivMember = (ImageView) newsDetailView.findViewById(R.id.iv_member);
        tvMemberName = (TextView) newsDetailView.findViewById(R.id.tv_memberName);
        tvMemberType = (TextView) newsDetailView.findViewById(R.id.tv_memberType);
        tvMemberSummary = (TextView) newsDetailView.findViewById(R.id.tv_memberSummary);
        tvMemberOrganizationCode = (TextView) newsDetailView.findViewById(R.id.tv_memberOrganizationCode);
        tvMemberAddress = (TextView) newsDetailView.findViewById(R.id.tv_memberAddress);
        tvMemberCorporateName = (TextView) newsDetailView.findViewById(R.id.tv_memberCorporateName);
        tvMemberContacts = (TextView) newsDetailView.findViewById(R.id.tv_memberContacts);
        tvMemberContactsPhone = (TextView) newsDetailView.findViewById(R.id.tv_memberContactsPhone);
        btnVisitURL = (Button) newsDetailView.findViewById(R.id.btn_visit_url);


        tvTitle.setText(getString(R.string.member_detail));
        tvClose.setVisibility(View.VISIBLE);
        tvClose.setBackgroundResource(R.mipmap.ico_close);

        funcGetData();
    }

    private void funcGetData() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            memberDetailPresenter.getPMmemberDetail(ctx, memberID);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }


    @Override
    public void updateMemberDetailView(String str) {
        member = GsonUtils.parseMember(str);

        String memberImgURL, memberName, memberType, memberSummary, memberOrganizationCode, memberAddress,
                memberCorporateName, memberContacts, memberContactsPhone;
        memberImgURL = member.getMImgURL() == null ? ctx.getString(R.string.null_value) : member.getMImgURL();
        memberName = member.getMName() == null ? ctx.getString(R.string.null_value) : member.getMName();
        memberType = member.getMTypeValue() == null ? ctx.getString(R.string.null_value) : member.getMTypeValue();
        memberSummary = member.getMSummary() == null ? ctx.getString(R.string.null_value) : member.getMSummary();
        memberOrganizationCode = member.getMOrganizationCode() == null ? ctx.getString(R.string.null_value) : member.getMOrganizationCode();
        memberAddress = member.getMAddress() == null ? ctx.getString(R.string.null_value) : member.getMAddress();
        memberCorporateName = member.getMCorporateName() == null ? ctx.getString(R.string.null_value) : member.getMCorporateName();
        memberContacts = member.getMContacts() == null ? ctx.getString(R.string.null_value) : member.getMContacts();
        memberContactsPhone = member.getMContactsPhone() == null ? ctx.getString(R.string.null_value) : member.getMContactsPhone();
        memberURL = member.getMURL() == null ? ctx.getString(R.string.null_value) : member.getMURL();

        Glide.with(ctx)
                .load(memberImgURL)
                .placeholder(R.mipmap.img_load)
                .error(R.mipmap.img_load_failed)
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(ivMember);
        tvMemberName.setText(memberName);
        tvMemberType.setText(memberType);
        tvMemberSummary.setText(String.format(getString(R.string.member_summary), memberSummary));
        tvMemberOrganizationCode.setText(String.format(getString(R.string.member_organizationCode), memberOrganizationCode));
        tvMemberAddress.setText(String.format(getString(R.string.member_address), memberAddress));
        tvMemberCorporateName.setText(String.format(getString(R.string.member_corporateName), memberCorporateName));
        tvMemberContacts.setText(String.format(getString(R.string.member_contacts), memberContacts));
        tvMemberContactsPhone.setText(String.format(getString(R.string.member_contactsPhone), memberContactsPhone));
        btnVisitURL.setOnClickListener(this);
        tvClose.setOnClickListener(this);
    }


    private void funcEnterMemberLink(String links) {
        Bundle trans = new Bundle();
        trans.putString(TransDefine.Bundle_Link_URL, links);
        trans.putString(TransDefine.Bundle_Link_Title, ctx.getString(R.string.member_home_page));
        WebViewFragment transFragment = new WebViewFragment();

        transFragment.setArguments(trans);
        getFragmentManager().beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, WebViewFragment.WEB_VIEW)
                .commit();
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

    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.tv_head_right:
                funcBack();
                break;
            case R.id.btn_visit_url:
                funcEnterMemberLink(memberURL);
                break;
            default:
                break;
        }
    }
}

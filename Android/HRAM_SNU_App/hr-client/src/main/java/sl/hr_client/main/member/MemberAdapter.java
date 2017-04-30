package sl.hr_client.main.member;

import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.FragmentManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;

import java.util.List;

import sl.hr_client.R;
import sl.hr_client.data.bean.LinkBean;
import sl.hr_client.data.bean.MemberBean;
import sl.hr_client.main.link.LinkAdapter;
import sl.hr_client.main.webview.WebViewFragment;
import sl.hr_client.net.member.detail.MemberDetailModel;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by Administrator on 2017/4/24.
 */

public class MemberAdapter extends RecyclerView.Adapter {
    private Context ctx;
    private FragmentManager fragmentManager;
    private List<MemberBean> memberList;

    public MemberAdapter(Context context, FragmentManager fragmentManager, List<MemberBean> memberList) {
        this.ctx = context;
        this.fragmentManager = fragmentManager;
        this.memberList = memberList;
    }


    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(ctx).inflate(R.layout.item_member, null);
        return new MemberHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        if (holder instanceof MemberHolder) {
            final MemberBean temp = memberList.get(position);
            String mImgUrl, mName, mApplyTime;
            mImgUrl = temp.getMImgURL() == null ? ctx.getString(R.string.null_value) : temp.getMImgURL();
            mName = temp.getMName() == null ? ctx.getString(R.string.null_value) : temp.getMName();
            mApplyTime = temp.getMApplyTime() == null ? ctx.getString(R.string.null_value) : temp.getMApplyTime();
            final String memberID = temp.getMemberID() == null ? ctx.getString(R.string.null_value) : temp.getMemberID();

            Glide.with(ctx)
                    .load(VolleyUtils.ServerIP + mImgUrl)
                    .placeholder(R.mipmap.img_load)
                    .error(R.mipmap.img_load_failed)
                    .diskCacheStrategy(DiskCacheStrategy.ALL)
                    .into(((MemberHolder) holder).ivMember);

            ((MemberHolder) holder).tvMemberName.setText(mName);
            ((MemberHolder) holder).tvMemberApplyTime.setText(mApplyTime);

            ((MemberHolder) holder).llMember.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    funcEnterMemberDetail(memberID);
                }
            });
        }
    }

    private void funcEnterMemberDetail(String memberID) {
        Bundle trans = new Bundle();
        trans.putString(TransDefine.Bundle_Member_ID, memberID);
        MemberDetailFragment transFragment = new MemberDetailFragment();

        transFragment.setArguments(trans);
        fragmentManager.beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, WebViewFragment.WEB_VIEW)
                .commit();
    }

    @Override
    public int getItemCount() {
        return memberList == null ? 0 : memberList.size();
    }

    public class MemberHolder extends RecyclerView.ViewHolder {
        private ImageView ivMember;
        private TextView tvMemberName;
        private TextView tvMemberApplyTime;
        private LinearLayout llMember;

        public MemberHolder(View itemView) {
            super(itemView);
            ivMember = (ImageView) itemView.findViewById(R.id.iv_member);
            tvMemberName = (TextView) itemView.findViewById(R.id.tv_member_name);
            tvMemberApplyTime = (TextView) itemView.findViewById(R.id.tv_member_applyTime);
            llMember = (LinearLayout) itemView.findViewById(R.id.ll_member);
        }
    }
}

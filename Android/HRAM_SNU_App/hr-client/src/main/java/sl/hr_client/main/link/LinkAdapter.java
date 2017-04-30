package sl.hr_client.main.link;

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
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.main.news.fragment.NewsDetailFragment;
import sl.hr_client.main.webview.WebViewFragment;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by Administrator on 2017/4/24.
 */

public class LinkAdapter extends RecyclerView.Adapter {
    private Context ctx;
    private FragmentManager fragmentManager;
    private List<LinkBean> linkList;

    public LinkAdapter(Context context, FragmentManager fragmentManager, List<LinkBean> linkList) {
        this.ctx = context;
        this.fragmentManager = fragmentManager;
        this.linkList = linkList;
    }


    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(ctx).inflate(R.layout.item_link, null);
        return new LinkHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        if (holder instanceof LinkHolder) {
            final LinkBean temp = linkList.get(position);
            String flImgUrl, flTypeValue, flName;
            flImgUrl = temp.getFlImgURL() == null ? ctx.getString(R.string.null_value) : temp.getFlImgURL();
            flTypeValue = temp.getFlTypeValue() == null ? ctx.getString(R.string.null_value) : temp.getFlTypeValue();
            flName = temp.getFlName() == null ? ctx.getString(R.string.null_value) : temp.getFlName();
            final String flURL = temp.getFlURL() == null ? ctx.getString(R.string.null_value) : temp.getFlURL();

            Glide.with(ctx)
                    .load(flImgUrl)
                    .placeholder(R.mipmap.img_load)
                    .error(R.mipmap.img_load_failed)
                    .diskCacheStrategy(DiskCacheStrategy.ALL)
                    .into(((LinkHolder) holder).ivLink);

            ((LinkHolder) holder).tvLinkType.setText(flTypeValue);
            ((LinkHolder) holder).tvLinkName.setText(flName);

            ((LinkHolder) holder).llLink.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    funcEnterLink(flURL);
                }
            });
        }
    }

    private void funcEnterLink(String links) {
        Bundle trans = new Bundle();
        trans.putString(TransDefine.Bundle_Link_URL, links);
        trans.putString(TransDefine.Bundle_Link_Title, ctx.getString(R.string.friendly_link_detail));
        WebViewFragment transFragment = new WebViewFragment();

        transFragment.setArguments(trans);
        fragmentManager.beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, WebViewFragment.WEB_VIEW)
                .commit();
    }

    @Override
    public int getItemCount() {
        return linkList == null ? 0 : linkList.size();
    }

    public class LinkHolder extends RecyclerView.ViewHolder {
        private ImageView ivLink;
        private TextView tvLinkType;
        private TextView tvLinkName;
        private LinearLayout llLink;

        public LinkHolder(View itemView) {
            super(itemView);
            ivLink = (ImageView) itemView.findViewById(R.id.iv_link);
            tvLinkType = (TextView) itemView.findViewById(R.id.tv_link_type);
            tvLinkName = (TextView) itemView.findViewById(R.id.tv_link_name);
            llLink = (LinearLayout) itemView.findViewById(R.id.ll_link);
        }
    }
}

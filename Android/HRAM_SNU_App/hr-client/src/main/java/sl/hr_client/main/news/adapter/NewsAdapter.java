package sl.hr_client.main.news.adapter;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.FragmentManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import java.util.List;

import sl.base.ui.noscroll.NoScrollGridView;
import sl.base.utils.UtilsHTMLSpirit;
import sl.hr_client.R;
import sl.hr_client.data.bean.ADImgBean;
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.main.news.fragment.NewsDetailFragment;
import sl.hr_client.main.photo.ImagePagerActivity;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by Administrator on 2017/4/6.
 * 暂时只有图片文字信息的
 */

public class NewsAdapter extends RecyclerView.Adapter {
    private Context ctx;
    private FragmentManager fragmentManager;
    private List<NewsBean> newsList;

    private int imgGvColumn = -1;//图片的GV的列数

    public NewsAdapter(Context context, FragmentManager fragmentManager, List<NewsBean> newsList) {
        this.ctx = context;
        this.fragmentManager = fragmentManager;
        this.newsList = newsList;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(ctx).inflate(R.layout.item_news, null);
        return new NewsTextHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        if (holder instanceof NewsTextHolder) {
            final NewsBean temp = newsList.get(position);
            String publishTime, username, publishType, publishTitle, publishText, publishViews;
            publishTime = temp.getPmPublishTime() == null ? ctx.getString(R.string.null_value) : temp.getPmPublishTime();
            username = temp.getUUserName() == null ? ctx.getString(R.string.null_value) : temp.getUUserName();
            publishType = temp.getPmTypeValue() == null ? ctx.getString(R.string.null_value) : temp.getPmTypeValue();
            publishTitle = temp.getPmTitle() == null ? ctx.getString(R.string.null_value) : temp.getPmTitle();
            publishText = temp.getPmText() == null ? ctx.getString(R.string.null_value) : temp.getPmText();
            publishViews = temp.getPmViews() == null ? ctx.getString(R.string.null_value) : temp.getPmViews();
            ((NewsTextHolder) holder).tvPublishTime.setText(publishTime);
            ((NewsTextHolder) holder).tvUsername.setText(username);
            ((NewsTextHolder) holder).tvPublishType.setText(String.format(ctx.getString(R.string.mid_bucket_format), publishType));
            ((NewsTextHolder) holder).tvPublishTitle.setText(publishTitle);

            publishText = UtilsHTMLSpirit.delHTMLTag(publishText);//去除HTML标签

            ((NewsTextHolder) holder).tvPublishText.setText(publishText);
            ((NewsTextHolder) holder).tvPublishViews.setText(publishViews);

            ((NewsTextHolder) holder).rlPublishInfo.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    funcEnterNewsDetail(temp.getPublishID());
                }
            });


            // 设置上传的图片
            if (temp.getPmADImgList() != null && temp.getPmADImgList().size() != 0) {//只有一条 且为""
                // 动态设置GridView列数
                ((NewsTextHolder) holder).gvPublishAdImg.setVisibility(View.VISIBLE);

                List<ADImgBean> adImgList = temp.getPmADImgList();

                if (temp.getPmADImgList().size() == 1) {
                    imgGvColumn = 1;
                }
                if (adImgList.size() == 2 || adImgList.size() == 4 ||
                        adImgList.size() == 5 || adImgList.size() == 7 ||
                        adImgList.size() == 8) {
                    imgGvColumn = 4;
                } else {
                    imgGvColumn = 3;
                }

                //根据图片数量设置GV
                ((NewsTextHolder) holder).gvPublishAdImg.setNumColumns(imgGvColumn);
                final String[] urls = new String[adImgList.size()];
                for (int i = 0; i < urls.length; i++) {
                    urls[i] = VolleyUtils.ServerIP + adImgList.get(i).getPmADImgListURL();
                }
                ((NewsTextHolder) holder).gvPublishAdImg.setAdapter(new CircleGridAdapter(ctx, urls));

                // 设置照片的点击事件
                ((NewsTextHolder) holder).gvPublishAdImg.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent,
                                            View view, int position, long id) {
                        enterPhotoDetailed(urls, position);
                    }
                });


            } else {
                ((NewsTextHolder) holder).gvPublishAdImg.setVisibility(View.GONE);
            }
        }
    }

    private void funcEnterNewsDetail(String publishID) {
        Bundle trans = new Bundle();
        trans.putString(TransDefine.Bundle_NewsID, publishID);
        NewsDetailFragment transFragment = new NewsDetailFragment();

        transFragment.setArguments(trans);
        fragmentManager.beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, NewsDetailFragment.NEWS_DETAIL)
                .commit();
    }

    @Override
    public int getItemCount() {
        return newsList == null ? 0 : newsList.size();
    }

    public class NewsTextHolder extends RecyclerView.ViewHolder {
        private ImageView ivPublishIcon;
        private TextView tvUsername;
        private TextView tvPublishType;
        private TextView tvPublishTitle;
        private TextView tvPublishTime;
        private TextView tvPublishText;
        private NoScrollGridView gvPublishAdImg;
        //        private TextView tvPublishGoodCount;
        private RelativeLayout rlPublishInfo;
        private TextView tvPublishViews;


        public NewsTextHolder(View itemView) {
            super(itemView);
            ivPublishIcon = (ImageView) itemView.findViewById(R.id.iv_publishIcon);
            tvUsername = (TextView) itemView.findViewById(R.id.tv_username);
            tvPublishType = (TextView) itemView.findViewById(R.id.tv_publishType);
            tvPublishTitle = (TextView) itemView.findViewById(R.id.tv_publishTitle);
            tvPublishTime = (TextView) itemView.findViewById(R.id.tv_publishTime);
            tvPublishText = (TextView) itemView.findViewById(R.id.tv_publishText);
            gvPublishAdImg = (NoScrollGridView) itemView.findViewById(R.id.gv_publishAdImg);
//            tvPublishGoodCount = (TextView) itemView.findViewById(R.id.tv_publishGoodCount);
            tvPublishViews = (TextView) itemView.findViewById(R.id.tv_publishViews);
            rlPublishInfo = (RelativeLayout) itemView.findViewById(R.id.rl_publish_info);
        }
    }


    /**
     * 进入图片详情页
     *
     * @param urls     图片数组
     * @param position 角标
     */
    protected void enterPhotoDetailed(String[] urls, int position) {
        Intent intent = new Intent(ctx, ImagePagerActivity.class);
        intent.putExtra(ImagePagerActivity.EXTRA_IMAGE_URLS, urls);
        intent.putExtra(ImagePagerActivity.EXTRA_IMAGE_INDEX, position);
        ctx.startActivity(intent);
    }
}

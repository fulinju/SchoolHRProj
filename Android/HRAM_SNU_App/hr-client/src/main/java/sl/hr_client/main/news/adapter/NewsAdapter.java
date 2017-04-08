package sl.hr_client.main.news.adapter;

import android.content.Context;
import android.support.v4.app.FragmentManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

import sl.base.ui.noscroll.NoScrollGridView;
import sl.hr_client.R;
import sl.hr_client.data.bean.ADImgBean;
import sl.hr_client.data.bean.NewsBean;

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
            ((NewsTextHolder) holder).tvPublishType.setText(String.format(ctx.getString(R.string.mid_bucket_format),publishType));
            ((NewsTextHolder) holder).tvPublishTitle.setText(publishTitle);
            ((NewsTextHolder) holder).tvPublishText.setText(publishText);
            ((NewsTextHolder) holder).tvPublishViews.setText(publishViews);

            // 设置上传的图片
            if (temp.getPmADImgList()!= null &&temp.getPmADImgList().size() != 0) {//只有一条 且为""
                // 动态设置gridview列数
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
                // 图片数组转图片集合
//                final String[] urls = adImgList.toArray(new String[adImgList.size()]);
                final String[] urls = new String[adImgList.size()];
                for (int i = 0 ; i< urls.length;i++){
                    urls[i] = adImgList.get(i).getPmADImgListURL();
                }
                ((NewsTextHolder) holder).gvPublishAdImg.setAdapter(new CircleGridAdapter(ctx, urls));

                // 设置点击事件
                ((NewsTextHolder) holder).gvPublishAdImg.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent,
                                            View view, int position, long id) {
//                        enterPhotoDetailed(urls, position);
                    }
                });

            } else {
                ((NewsTextHolder) holder).gvPublishAdImg.setVisibility(View.GONE);
            }
        }
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
        private TextView tvPublishGoodCount;
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
            tvPublishGoodCount = (TextView) itemView.findViewById(R.id.tv_publishGoodCount);
            tvPublishViews = (TextView) itemView.findViewById(R.id.tv_publishViews);
        }
    }
}

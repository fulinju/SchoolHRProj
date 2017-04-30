package sl.hr_client.main.news.fragment;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.TextView;

import java.util.List;

import sl.base.ui.noscroll.NoScrollGridView;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.data.bean.ADImgBean;
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.data.parse.GsonUtils;
import sl.hr_client.main.news.adapter.CircleGridAdapter;
import sl.hr_client.main.news.adapter.NewsAdapter;
import sl.hr_client.main.photo.ImagePagerActivity;
import sl.hr_client.net.news.detail.NewsDetailPresenter;
import sl.hr_client.net.news.detail.NewsDetailView;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by Administrator on 2017/4/12.
 */

public class NewsDetailFragment extends BaseFragment implements NewsDetailView {
    public static final String NEWS_DETAIL = "NEWS_DETAIL";

    public String newsID;

    private View newsDetailView;
    private Context ctx;

    private NewsDetailPresenter newsDetailPresenter;

    private TextView tvTitle;
    private TextView tvClose;

    private TextView tvPublishTitle;
    private TextView tvPublishTime;
    private TextView tvPublishText;
    private NoScrollGridView gvPublishAdImg;
    private TextView tvPublishViews;

    private NewsBean news;

    private int imgGvColumn = -1;//图片的GV的列数


    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        newsDetailView = inflater.inflate(R.layout.fragment_news_detail, container, false);

        ctx = newsDetailView.getContext();
        newsDetailPresenter= new NewsDetailPresenter(this);

        funcGetIntent();
        initView();
        return newsDetailView;
    }

    private void funcGetIntent(){
        newsID = getArguments().getString(TransDefine.Bundle_NewsID);
        if(newsID == null){
            UtilsToast.showToast(ctx,getString(R.string.cant_get_publishId));
            funcBack();
        }
    }

    private void initView(){
        tvTitle = (TextView) newsDetailView.findViewById(R.id.tv_head);
        tvClose = (TextView) newsDetailView.findViewById(R.id.tv_head_right);

        tvPublishTitle = (TextView) newsDetailView.findViewById(R.id.tv_publishTitle);
        tvPublishTime = (TextView) newsDetailView.findViewById(R.id.tv_publishTime);
        tvPublishText = (TextView) newsDetailView.findViewById(R.id.tv_publishText);
        gvPublishAdImg = (NoScrollGridView) newsDetailView.findViewById(R.id.gv_publishAdImg);
        tvPublishViews = (TextView) newsDetailView.findViewById(R.id.tv_publishViews);

        tvTitle.setText(getString(R.string.publish_detail));
        tvClose.setVisibility(View.VISIBLE);
        tvClose.setBackgroundResource(R.mipmap.ico_close);

        tvClose.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                funcBack();
            }
        });
        funcGetData();
    }

    private void funcGetData() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            newsDetailPresenter.getPNewsDetailDetail(ctx, newsID);
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private void funcBack(){
        getFragmentManager().popBackStack();
    }

    @Override
    public void updateNewsDetailView(String str) {
        news = GsonUtils.parseNews(str);
        String publishTime, publishTitle, publishText, publishViews;
        publishTime = news.getPmPublishTime() == null ? ctx.getString(R.string.null_value) : news.getPmPublishTime();
        publishTitle = news.getPmTitle() == null ? ctx.getString(R.string.null_value) : news.getPmTitle();
        publishText = news.getPmText() == null ? ctx.getString(R.string.null_value) : news.getPmText();
        publishViews = news.getPmViews() == null ? ctx.getString(R.string.null_value) : news.getPmViews();

        tvPublishTitle.setText(publishTitle);
        tvPublishText.setText(publishText);
        tvPublishTime.setText(publishTime);
        tvPublishViews.setText(publishViews);


        if (news.getPmADImgList() != null && news.getPmADImgList().size() != 0) {//只有一条 且为""
            // 动态设置gridview列数
            gvPublishAdImg.setVisibility(View.VISIBLE);

            List<ADImgBean> adImgList = news.getPmADImgList();

            if (news.getPmADImgList().size() == 1) {
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
            gvPublishAdImg.setNumColumns(imgGvColumn);
            final String[] urls = new String[adImgList.size()];
            for (int i = 0; i < urls.length; i++) {
                urls[i] = VolleyUtils.ServerIP + adImgList.get(i).getPmADImgListURL();
            }
            gvPublishAdImg.setAdapter(new CircleGridAdapter(ctx, urls));

            // 设置照片的点击事件
            gvPublishAdImg.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent,
                                        View view, int position, long id) {
                    enterPhotoDetailed(urls, position);
                }
            });


        } else {
            gvPublishAdImg.setVisibility(View.GONE);
        }

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

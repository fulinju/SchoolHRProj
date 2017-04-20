package sl.hr_client.main.news.fragment;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;

/**
 * Created by Administrator on 2017/4/12.
 */

public class NewsDetailFragment extends BaseFragment  {
    public static final String NEWS_DETAIL = "NEWS_DETAIL";

    private View newsDetailView;
    private Context ctx;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        newsDetailView = inflater.inflate(R.layout.fragment_news_detail, container, false);

        ctx = newsDetailView.getContext();
        return newsDetailView;
    }
}

package sl.hr_client.net.news.detail;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/12.
 */

public class NewsDetailPresenter {
    private NewsDetailView newsDetailView;
    private NewsDetailModel newsDetailModel;

    public NewsDetailPresenter(NewsDetailView newsDetailView) {
        this.newsDetailView = newsDetailView;
        newsDetailModel = new NewsDetailModel();
    }

    public void getPNewsDetailDetail(Context context, String publishID) {
        newsDetailView.showLoading();

        newsDetailModel.getNewsDetail(context,publishID)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        newsDetailView.updateNewsDetailView(s);
                    }

                    @Override
                    public void onCompleted() {
                        newsDetailView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        newsDetailView.showError(e.getMessage());
                    }
                });
    }
}

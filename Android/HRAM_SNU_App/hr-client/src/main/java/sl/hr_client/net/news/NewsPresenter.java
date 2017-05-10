package sl.hr_client.net.news;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/6.
 */

public class NewsPresenter {
    private NewsView newsView;
    private NewsModel newsModel;

    public NewsPresenter(NewsView newsView) {
        this.newsView = newsView;
        newsModel = new NewsModel();
    }

    public void getPNews(Context context,int pageIndex,int pageSize) {
        newsView.showLoading();

        newsModel.getNews(context,pageIndex,pageSize)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        newsView.updateNewsView(s);
                    }

                    @Override
                    public void onCompleted() {
                        newsView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        newsView.showError(e.getMessage());
                    }
                });
    }

    public void loadMorePNews(Context context,int pageIndex,int pageSize) {
        newsView.showLoading();

        newsModel.getNews(context,pageIndex,pageSize)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        newsView.loadMoreNewsView(s);
                    }

                    @Override
                    public void onCompleted() {
                        newsView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        newsView.showError(e.getMessage());
                    }
                });
    }
}

package sl.hr_client.net.link;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/24.
 */

public class LinkPresenter {
    private LinkView linkView;
    private LinkModel linkModel;

    public LinkPresenter(LinkView linkView) {
        this.linkView = linkView;
        linkModel = new LinkModel();
    }

    public void getPLink(Context context, int pageIndex, int pageSize) {
        linkView.showLoading();

        linkModel.getLinks(context,pageIndex,pageSize)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        linkView.updateLinksView(s);
                    }

                    @Override
                    public void onCompleted() {
//                        linkView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        linkView.showError(e.getMessage());
                    }
                });
    }

    public void loadMorePLink(Context context, int pageIndex, int pageSize) {
        linkView.showLoading();

        linkModel.getLinks(context,pageIndex,pageSize)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        linkView.updateLoadMoreLinksView(s);
                    }

                    @Override
                    public void onCompleted() {
                        linkView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        linkView.showError(e.getMessage());
                    }
                });
    }
}

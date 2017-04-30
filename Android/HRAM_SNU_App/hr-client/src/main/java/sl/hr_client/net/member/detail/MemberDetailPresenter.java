package sl.hr_client.net.member.detail;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/24.
 */

public class MemberDetailPresenter {
    private MemberDetailView memberDetailView;
    private MemberDetailModel memberDetailModel;

    public MemberDetailPresenter(MemberDetailView memberDetailView) {
        this.memberDetailView = memberDetailView;
        memberDetailModel = new MemberDetailModel();
    }

    public void getPMmemberDetail(Context context,  String memberIDe) {
        memberDetailView.showLoading();

        memberDetailModel.getMemberDetail(context,memberIDe)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        memberDetailView.updateMemberDetailView(s);
                    }

                    @Override
                    public void onCompleted() {
                        memberDetailView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        memberDetailView.showError(e.getMessage());
                    }
                });
    }
}

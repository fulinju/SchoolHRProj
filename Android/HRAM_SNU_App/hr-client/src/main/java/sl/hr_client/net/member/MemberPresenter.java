package sl.hr_client.net.member;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/24.
 */

public class MemberPresenter {
    private MemberView memberView;
    private MemberModel memberModel;

    public MemberPresenter(MemberView memberView) {
        this.memberView = memberView;
        memberModel = new MemberModel();
    }

    public void getPMember(Context context, int pageIndex, int pageSize) {
        memberView.showLoading();

        memberModel.getMembers(context,pageIndex,pageSize)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        memberView.updateMembersView(s);
                    }

                    @Override
                    public void onCompleted() {
                        memberView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        memberView.showError(e.getMessage());
                    }
                });
    }
}

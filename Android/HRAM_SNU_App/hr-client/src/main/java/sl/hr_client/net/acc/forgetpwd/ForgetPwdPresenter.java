package sl.hr_client.net.acc.forgetpwd;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/5/2.
 */

public class ForgetPwdPresenter {

    private ForgetPwdView forgetPwdView;
    private ForgetPwdModel forgetPwdModel;

    public ForgetPwdPresenter(ForgetPwdView forgetPwdView) {
        this.forgetPwdView = forgetPwdView;
        forgetPwdModel = new ForgetPwdModel();
    }

    public void pForgetPwd(Context context,
                           String uMaiBox, String uLoginStr,
                           String uClientKey) {
        forgetPwdView.showLoading();

        forgetPwdModel.forgetPwd(context, uMaiBox, uLoginStr, uClientKey)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        forgetPwdView.resetSuccessView(s);
                    }

                    @Override
                    public void onCompleted() {
                        forgetPwdView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        forgetPwdView.showError(e.getMessage());
                    }
                });
    }
}

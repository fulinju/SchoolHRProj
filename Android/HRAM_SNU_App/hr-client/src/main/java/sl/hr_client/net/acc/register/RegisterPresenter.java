package sl.hr_client.net.acc.register;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/25.
 */

public class RegisterPresenter {
    private RegisterView registerView;
    private RegisterModel registerModel;

    public RegisterPresenter(RegisterView registerView) {
        this.registerView = registerView;
        registerModel = new RegisterModel();
    }

    public void pRegisterByMail(Context context, String mailbox) {
        registerView.showLoading();

        registerModel.registerByMail(context, mailbox)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        registerView.registerSuccessView(s);
                    }

                    @Override
                    public void onCompleted() {
                        registerView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        registerView.showError(e.getMessage());
                    }
                });
    }


    public void pRegisterByRegisterByNameAndPwd(Context context, String uLoginName, String uPassword) {
        registerView.showLoading();

        registerModel.registerByRegisterByNameAndPwd(context, uLoginName, uPassword)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        registerView.registerSuccessView(s);
                    }

                    @Override
                    public void onCompleted() {
                        registerView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        registerView.showError(e.getMessage());
                    }
                });
    }
}

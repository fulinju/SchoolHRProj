package sl.hr_client.net.acc.login;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/25.
 */

public class LoginPresenter {
    private LoginView loginView;
    private LoginModel loginModel;

    public LoginPresenter(LoginView loginView) {
        this.loginView = loginView;
        loginModel = new LoginModel();
    }

    public void pLogin(Context context, String uLoginStr, String uPassword, String uClientKey) {
        loginView.showLoading();

        loginModel.login(context, uLoginStr, uPassword, uClientKey)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        loginView.loginSuccessView(s);
                    }

                    @Override
                    public void onCompleted() {
                        loginView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        loginView.showError(e.getMessage());
                    }
                });
    }
}

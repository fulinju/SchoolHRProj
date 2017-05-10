package sl.hr_client.net.acc.modifypwd;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;
import sl.base.utils.UtilsMD5;

/**
 * Created by Administrator on 2017/5/2.
 */

public class ModifyPwdPresenter {

    private ModifyPwdView modifyPwdView;
    private ModifyPwdModel modifyPwdModel;

    public ModifyPwdPresenter(ModifyPwdView modifyPwdView) {
        this.modifyPwdView = modifyPwdView;
        modifyPwdModel = new ModifyPwdModel();
    }

    public void pModifyPwd(Context context, String uID,
                           String uLoginStr, String uOldPassword,
                           String uNewPassword, String uClientKey) {
        modifyPwdView.showLoading();

        String oldPwdMd5 = UtilsMD5.getMD5_UpperCase(uOldPassword);
        String newPwdMd5 = UtilsMD5.getMD5_UpperCase(uNewPassword);

        modifyPwdModel.modifyPwd(context, uID, uLoginStr, oldPwdMd5,newPwdMd5,uClientKey)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        modifyPwdView.modifyPwdSuccessView(s);
                    }

                    @Override
                    public void onCompleted() {
                        modifyPwdView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        modifyPwdView.showError(e.getMessage());
                    }
                });
    }
}

package sl.hr_client.net.acc.modifyinfo;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/5/2.
 */

public class ModifyInfoPresenter {
    private ModifyInfoView modifyInfoView;
    private ModifyInfoModel modifyInfoModel;

    public ModifyInfoPresenter(ModifyInfoView modifyInfoView) {
        this.modifyInfoView = modifyInfoView;
        modifyInfoModel = new ModifyInfoModel();
    }

    public void pModifyInfo(Context context, String uID,
                            String uMaiBox, String uPhone,
                            String uUserName, String uClientKey) {
        modifyInfoView.showLoading();

        modifyInfoModel.modifyUserInfo(context, uID, uMaiBox, uPhone, uUserName, uClientKey)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        modifyInfoView.modifyInfoSuccessView(s);
                    }

                    @Override
                    public void onCompleted() {
                        modifyInfoView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        modifyInfoView.showError(e.getMessage());
                    }
                });
    }
}

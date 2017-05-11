package sl.hr_client.main.setting;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.greenrobot.eventbus.EventBus;

import sl.base.utils.UtilsKeyBoard;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.base.utils.UtilsValidate;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.imp.FragmentBackListener;
import sl.hr_client.main.MainActivity;
import sl.hr_client.net.acc.modifyinfo.ModifyInfoPresenter;
import sl.hr_client.net.acc.modifyinfo.ModifyInfoView;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.ResponseUtils;
import sl.hr_client.utils.ui.WaitingDialog;

/**
 * Created by Administrator on 2017/5/3.
 */

public class UserInfoFragment extends BaseFragment implements View.OnClickListener, ModifyInfoView ,FragmentBackListener{
    public static final String USER_INFO = "User_Info";

    private View userInfoView;
    private Context ctx;

    private TextView tvTitle;
    private TextView tvBack;

    private TextView tvLoginNameDes;
    private EditText edtUsername;
    private RelativeLayout rlSave;

    private ModifyInfoPresenter modifyInfoPresenter;


    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        tvTitle = (TextView) userInfoView.findViewById(R.id.tv_head);
        tvBack = (TextView) userInfoView.findViewById(R.id.tv_head_right);

        tvLoginNameDes = (TextView) userInfoView.findViewById(R.id.tv_loginName_des);
        edtUsername = (EditText) userInfoView.findViewById(R.id.edt_username);
        rlSave = (RelativeLayout) userInfoView.findViewById(R.id.rl_save);

        tvTitle.setText(getString(R.string.account_info));
        tvBack.setText(getString(R.string.back));
        tvBack.setVisibility(View.VISIBLE);

        String uUsername = DataUtils.getNowUserByUID().getUUserName() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUUserName();
        String uLoginName = DataUtils.getNowUserByUID().getULoginStr() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getULoginStr();

        tvLoginNameDes.setText(uLoginName);
        edtUsername.setText(uUsername);

        WaitingDialog.createWaitingDlg(ctx, getString(R.string.saving));

        addListener();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        userInfoView = inflater.inflate(R.layout.fragment_user_info, container, false);

        ctx = userInfoView.getContext();
        modifyInfoPresenter = new ModifyInfoPresenter(this);
        return userInfoView;
    }

    private void addListener() {
        tvLoginNameDes.setOnClickListener(this);
        rlSave.setOnClickListener(this);
        tvBack.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.tv_head_right:
                funcBack();
                break;
            case R.id.rl_save:
                funcSave();
                break;
            case R.id.tv_loginName_des:
                funcModifyUsername();
                break;
            default:
                break;
        }
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }

    private void funcSave() {
//        edtUsername.clearFocus();
        if (UtilsNet.isNetworkAvailable(ctx)) {
            String uID = DataUtils.getNowUserByUID().getUID() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUID();
            String uUsername = edtUsername.getText() == null ? getString(R.string.null_value) : edtUsername.getText().toString();
            String uMailbox = DataUtils.getNowUserByUID().getUMaiBox() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUMaiBox();
            String uPhone = DataUtils.getNowUserByUID().getUPhone() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUPhone();
            String uClientKey = UtilsPreference.getString(ConstantData.FLAG_GE_TUI_CLIENTID, ConstantData.default_String);

            boolean inputFlag = checkInput(uUsername);
            if (inputFlag) {
                modifyInfoPresenter.pModifyInfo(ctx, uID, uMailbox, uPhone, uUsername, uClientKey);

                WaitingDialog.showWaitingDlg();
            }
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private void funcModifyUsername() {
        UtilsToast.showToast(ctx, String.format(getString(R.string.cant_modify), getString(R.string.username)));
    }

    private boolean checkInput(String uUsername) {
        UtilsKeyBoard.hideKeyBoard(getActivity());

        if (!UtilsValidate.checkInputRange(uUsername, ConstantData.NICKNAME_MIN_LENGTH, ConstantData.NICKNAME_MAX_LENGTH)) {
            UtilsToast.showToast(ctx, String.format(getString(R.string.input_length_between),
                    getString(R.string.nickname), ConstantData.NICKNAME_MIN_LENGTH, ConstantData.NICKNAME_MAX_LENGTH));
            return false;
        }
        return true;
    }


    @Override
    public void modifyInfoSuccessView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        ResponseUtils.showResponseOperate(ctx, str);
        UserBean user = DataUtils.getNowUserByUID();
        String uUsername = edtUsername.getText() == null ? getString(R.string.null_value) : edtUsername.getText().toString();
        user.setUUserName(uUsername);
        DataUtils.updateUser(user);
        EventBus.getDefault().post(
                new TransferEvent(TransDefine.EVENT_MODIFY_USER_INFO)); //发送EventBus消息

        WaitingDialog.dismissWaitingDlg();

    }

    @Override
    public void showLoading() {

    }

    @Override
    public void hideLoading() {

    }

    @Override
    public void showError(String msg) {
        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
        WaitingDialog.dismissWaitingDlg();
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);

        if (context instanceof MainActivity) {
            ((MainActivity) context).setBackListener(this);
            ((MainActivity) context).setInterception(true);
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        if(getActivity() instanceof MainActivity){
            ((MainActivity)getActivity()).setBackListener(null);
            ((MainActivity)getActivity()).setInterception(false);
        }
    }

    @Override
    public void onBackForward() {
        funcBack();
    }
}

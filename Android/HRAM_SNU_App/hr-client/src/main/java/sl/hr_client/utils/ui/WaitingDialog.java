package sl.hr_client.utils.ui;

import android.app.Dialog;
import android.content.Context;
import android.widget.TextView;

import sl.hr_client.R;

/**
 * Created by Administrator on 2017/5/8.
 */

public class WaitingDialog {
    public static Dialog progressDialog;

    public static void createWaitingDlg(Context ctx, String message) {
        progressDialog = new Dialog(ctx, R.style.progress_dialog);
        progressDialog.setContentView(R.layout.layout_wait_dlg);
        progressDialog.getWindow().setBackgroundDrawableResource(android.R.color.transparent);
        TextView msg = (TextView) progressDialog.findViewById(R.id.id_tv_loading_msg);
        msg.setText(message);
    }

    public static void showWaitingDlg() {
        progressDialog.show();
    }

    public static void dismissWaitingDlg() {
        progressDialog.dismiss();
    }
}

package sl.hr_client.net.download;

import android.app.ProgressDialog;
import android.content.Context;

import org.xutils.HttpManager;
import org.xutils.common.Callback;
import org.xutils.http.RequestParams;
import org.xutils.x;

import java.io.File;

import sl.base.utils.UtilsToast;

/**
 * Created by Administrator on 2017/4/12.
 */

public class DownloadUtils {

    public static void downloadFile(final Context context, final String url, String path) {
        final ProgressDialog  progressDialog = new ProgressDialog(context);
        RequestParams requestParams = new RequestParams(url);
        requestParams.setSaveFilePath(path);
        x.http().get(requestParams, new Callback.ProgressCallback<File>() {
            @Override
            public void onWaiting() {
            }

            @Override
            public void onStarted() {
            }

            @Override
            public void onLoading(long total, long current, boolean isDownloading) {
                progressDialog.setProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
                progressDialog.setMessage("亲，努力下载中。。。");
                progressDialog.show();
                progressDialog.setMax((int) total);
                progressDialog.setProgress((int) current);
            }

            @Override
            public void onSuccess(File result) {
                UtilsToast.showToast(context,"下载成功");
                progressDialog.dismiss();
            }

            @Override
            public void onError(Throwable ex, boolean isOnCallback) {
                ex.printStackTrace();
                UtilsToast.showToast(context,"下载失败，请检查网络和SD卡");
                progressDialog.dismiss();
            }

            @Override
            public void onCancelled(CancelledException cex) {
            }

            @Override
            public void onFinished() {
            }
        });
    }
}

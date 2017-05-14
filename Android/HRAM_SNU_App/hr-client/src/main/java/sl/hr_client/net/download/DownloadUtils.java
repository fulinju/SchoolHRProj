package sl.hr_client.net.download;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.support.v7.app.NotificationCompat;

import org.greenrobot.eventbus.EventBus;
import org.xutils.common.Callback;
import org.xutils.http.RequestParams;
import org.xutils.x;

import java.io.File;

import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.VolleyUtils;

import static android.R.attr.id;

/**
 * Created by Administrator on 2017/4/12.
 */

public class DownloadUtils {
    public static void downloadFile(final Context context, final String url,
                                    final String path, final String dmTitle,
                                    final String downloadID) {
        final NotificationManager mNotifyManager;
        final NotificationCompat.Builder mBuilder;
//        RemoteViews mRemoteViews = new RemoteViews(context.getPackageName(), R.layout.notification_download);
//
//        //API3.0 以上的时候显示按钮，否则消失
//        mRemoteViews.setTextViewText(R.id.tv_notification_title, "正在下载");
//        mRemoteViews.setTextViewText(R.id.tv_notification_content, dmTitle);
//        //如果版本号低于（3.0），那么不显示按钮
//        if (UtilsVersion.getSystemVersion() <= 9) {
//            mRemoteViews.setViewVisibility(R.id.btn_notification_state, View.GONE);
//        } else {
//            mRemoteViews.setViewVisibility(R.id.btn_notification_state, View.VISIBLE);
//        }
//
//        mRemoteViews.setProgressBar(R.id.rpb_notification_download, 0, 0, false);

        mNotifyManager = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
        mBuilder = new NotificationCompat.Builder(context);
        mBuilder.setContentTitle(context.getString(R.string.downloading) + dmTitle).setSmallIcon(R.mipmap.ic_launcher);


        RequestParams requestParams = new RequestParams(VolleyUtils.ServerIP + url);
        requestParams.setSaveFilePath(path);

        x.http().get(requestParams, new Callback.ProgressCallback<File>() {
            @Override
            public void onWaiting() {
            }

            @Override
            public void onStarted() {
                //开始时候，初始化进度条
                mBuilder.setProgress(100, 0, false);
                mNotifyManager.notify(id, mBuilder.build());
            }

            @Override
            public void onLoading(long total, long current, boolean isDownloading) {
                //更新进度条
                int currentNum = (int) (100 * current / total);
                mBuilder.setProgress(100, currentNum, false);
                mBuilder.setContentText(currentNum + context.getString(R.string.sign_percent));
                mNotifyManager.notify(id, mBuilder.build());
            }

            @Override
            public void onSuccess(File result) {
                String fileType = url.substring(url.lastIndexOf(".") + 1);
                File newFile = new File(path + dmTitle + "." + fileType);
                result.renameTo(newFile);//重命名

                //成功后将setProgress的两个变量设置为0，progress就会消失。setProgress第三个参数的含义是，如果能确定进度条执行的时间，就传入true，否则是false，此处直接传入false即可。
                mBuilder.setProgress(0, 0, false).setContentTitle(dmTitle).setContentText(context.getString(R.string.download_succeed));

                Intent intent = openAssignFolder(newFile.getPath());
                PendingIntent pendingIntent = PendingIntent.getActivity(context, 0, intent, 0);
                mBuilder.setContentIntent(pendingIntent);
                mNotifyManager.notify(id, mBuilder.build());

                EventBus.getDefault().post(
                        new TransferEvent(TransDefine.EVENT_DOWNLOAD_SUCCEED, downloadID)); //通知下载完成
            }

            @Override
            public void onError(Throwable ex, boolean isOnCallback) {
                ex.printStackTrace();
                mNotifyManager.cancel(id);
                UtilsToast.showToast(context, context.getString(R.string.download_failed));
            }

            @Override
            public void onCancelled(CancelledException cex) {
            }

            @Override
            public void onFinished() {

            }
        });
    }

    private static Intent openAssignFolder(String path) {
        File file = new File(path);
        Intent intent = new Intent(Intent.ACTION_GET_CONTENT);
        intent.addCategory(Intent.CATEGORY_DEFAULT);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        intent.setDataAndType(Uri.fromFile(file), "file/*");
        return intent;
    }

}

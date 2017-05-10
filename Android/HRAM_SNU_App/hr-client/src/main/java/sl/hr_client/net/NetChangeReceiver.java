package sl.hr_client.net;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.view.View;

import org.greenrobot.eventbus.EventBus;

import sl.base.utils.UtilsNet;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.utils.constant.TransDefine;

/**
 * Created by Administrator on 2017/5/8.
 */

public class NetChangeReceiver extends BroadcastReceiver {
    @Override
    public void onReceive(Context context, Intent intent) {
        EventBus.getDefault().post(
                new TransferEvent(TransDefine.EVENT_MODIFY_NET));
    }
}

package sl.hr_client.main.push;

import android.content.Context;
import com.igexin.sdk.GTIntentService;
import com.igexin.sdk.message.GTCmdMessage;
import com.igexin.sdk.message.GTTransmitMessage;

import org.greenrobot.eventbus.EventBus;

import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsPreference;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.constant.TransDefine;

/**
 * 继承 GTIntentService 接收来自个推的消息, 所有消息在线程中回调, 如果注册了该服务, 则务必要在 AndroidManifest中声明, 否则无法接受消息<br>
 * onReceiveMessageData 处理透传消息<br>
 * onReceiveClientId 接收 cid <br>
 * onReceiveOnlineState cid 离线上线通知 <br>
 * onReceiveCommandResult 各种事件处理回执 <br>
 */
public class GeTuiIntentService extends GTIntentService {
    private String clientID; //客户端ID

    public GeTuiIntentService() {

    }

    @Override
    public void onReceiveServicePid(Context context, int pid) {

    }

    @Override
    public void onReceiveMessageData(Context context, GTTransmitMessage msg) {
        String appid = msg.getAppid();
        String taskid = msg.getTaskId();
        String messageid = msg.getMessageId();
        byte[] payload = msg.getPayload();
        String pkg = msg.getPkgName();
        String cid = msg.getClientId();

        String oIDStr = new String(payload);

        int oID = oIDStr == null ? 0 : Integer.parseInt(oIDStr);
        UtilsLog.logE(UtilsLog.getSte(), oID);
    }

    @Override
    public void onReceiveClientId(Context context, String clientID) {
//        UtilsLog.logE(UtilsLog.getSte(), clientID);
        UtilsPreference.commitString(ConstantData.FLAG_GE_TUI_CLIENTID,clientID);
        EventBus.getDefault().post(
                new TransferEvent(TransDefine.EVENT_GET_PUSH_ID)); //发送EventBus消息
    }

    @Override
    public void onReceiveOnlineState(Context context, boolean online) {
    }

    @Override
    public void onReceiveCommandResult(Context context, GTCmdMessage cmdMessage) {
    }
}
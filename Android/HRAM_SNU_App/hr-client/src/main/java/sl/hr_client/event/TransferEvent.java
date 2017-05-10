package sl.hr_client.event;

/**
 * Created by Administrator on 2017/5/8.
 */

public class TransferEvent {
    private String msg;
    private String targetTag;


    public TransferEvent(String targetTag) {
        this.targetTag = targetTag;
    }

    public TransferEvent(String targetTag,String msg) {
        this.msg = msg;
        this.targetTag = targetTag;
    }

    public String getMsg() {
        return msg;
    }

    public String getTargetTag() {
        return targetTag;
    }
}

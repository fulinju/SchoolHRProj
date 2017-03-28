package sl.base.utils;

/**
 * Created by xuzhijix on 2016/12/12.
 * API的参数
 */
public class UtilsApi {
    private static final String SERVER_IP = "http://172.17.30.181";
    private static final String SERVER_PORT = "8888";

    public static final String getServerAddress() {
        return SERVER_IP + ":" + SERVER_PORT;
    }
}

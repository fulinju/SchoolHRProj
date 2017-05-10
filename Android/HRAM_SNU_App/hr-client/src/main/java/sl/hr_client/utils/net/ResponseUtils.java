package sl.hr_client.utils.net;

import android.content.Context;

import com.android.volley.VolleyError;
import com.google.gson.Gson;

import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.data.bean.ResponseBean;

/**
 * Created by Administrator on 2017/5/1.
 */

public class ResponseUtils {

    public static String responseOperate(VolleyError volleyError) {
        String errorType = "";
        if (volleyError.networkResponse == null) {
            errorType = VolleyUtils.ErrVolleyType;
        } else {
            errorType = new String(volleyError.networkResponse.data);
        }
        return errorType;
    }

    public static void showResponseOperate(Context context, String msg) {
        if (msg.equals(VolleyUtils.ErrVolleyType)) {
            UtilsToast.showToast(context, context.getString(R.string.network_err));
        } else {
            ResponseBean temp = parseResponse(msg);
            String responseID = temp.getState();
            UtilsToast.showToast(context, temp.getMessage());
        }
    }

    /**
     * 解析ResponseBean
     *
     * @param json
     * @return
     */
    public static ResponseBean parseResponse(String json) {
        ResponseBean error = null;
        Gson gson = new Gson();
        error = gson.fromJson(json, ResponseBean.class);
        return error;
    }
}

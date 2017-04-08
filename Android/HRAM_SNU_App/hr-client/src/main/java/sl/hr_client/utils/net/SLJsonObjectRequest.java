package sl.hr_client.utils.net;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.Response;
import com.android.volley.RetryPolicy;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonRequest;

import org.json.JSONException;
import org.json.JSONObject;

import sl.hr_client.utils.constant.ConstantData;


/**
 * Created by Administrator on 2017/3/5.
 */
public class SLJsonObjectRequest extends JsonRequest<JSONObject> {
    public SLJsonObjectRequest(int method, String url, String requestBody, Response.Listener<JSONObject> listener, Response.ErrorListener errorListener) {
        super(method, url, requestBody, listener, errorListener);
    }

    public SLJsonObjectRequest(String url, Response.Listener<JSONObject> listener, Response.ErrorListener errorListener) {
        super(0, url, (String) null, listener, errorListener);
    }

    public SLJsonObjectRequest(int method, String url, Response.Listener<JSONObject> listener, Response.ErrorListener errorListener) {
        super(method, url, (String) null, listener, errorListener);
    }

    public SLJsonObjectRequest(int method, String url, JSONObject jsonRequest, Response.Listener<JSONObject> listener, Response.ErrorListener errorListener) {
        super(method, url, jsonRequest == null ? null : jsonRequest.toString(), listener, errorListener);
    }

    public SLJsonObjectRequest(String url, JSONObject jsonRequest, Response.Listener<JSONObject> listener, Response.ErrorListener errorListener) {
        this(jsonRequest == null ? 0 : 1, url, jsonRequest, listener, errorListener);
    }

    /**
     * 重新封装，解析NetworkResponse
     *
     * @param response
     * @return
     */
    protected Response<JSONObject> parseNetworkResponse(NetworkResponse response) {
        try {
//            String je = new String(response.data,"UTF-8");
            String je = new String(response.data);
            //移除前后多余的双引号
            je = je.substring(1, je.length() - 1);
            //去掉双引号转义
            je = je.replace("\\\"", "\"");

            JSONObject json = new JSONObject(je);

            return Response.success(json, HttpHeaderParser.parseCacheHeaders(response));
        } catch (JSONException var4) {
            return Response.error(new ParseError(var4));
        }
    }

    @Override
    public RetryPolicy getRetryPolicy() {
        RetryPolicy retryPolicy = new DefaultRetryPolicy(ConstantData.requestTimeOut, DefaultRetryPolicy.DEFAULT_MAX_RETRIES, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT);
        return retryPolicy;
    }
}

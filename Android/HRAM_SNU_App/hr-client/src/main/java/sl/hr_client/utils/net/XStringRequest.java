package sl.hr_client.utils.net;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.Response;
import com.android.volley.RetryPolicy;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;

import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by xuzhijix on 2017/3/10.
 */
public class XStringRequest extends StringRequest {
    public XStringRequest(int method, String url, Response.Listener<String> listener, Response.ErrorListener errorListener) {
        super(method, url, listener, errorListener);

    }

    public XStringRequest(String url, Response.Listener<String> listener, Response.ErrorListener errorListener) {
        super(url, listener, errorListener);
    }

    @Override
    protected Response<String> parseNetworkResponse(NetworkResponse response) {
        return super.parseNetworkResponse(response);
    }

    @Override
    protected VolleyError parseNetworkError(VolleyError volleyError) {
        return super.parseNetworkError(volleyError);
    }

    @Override
    public RetryPolicy getRetryPolicy() {
        //确保最大重试次数为1，以保证超时后不重新请求
        RetryPolicy retryPolicy = new DefaultRetryPolicy(ConstantData.requestTimeOut, ConstantData.requestRetryTimes, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT);
        return retryPolicy;
    }
}

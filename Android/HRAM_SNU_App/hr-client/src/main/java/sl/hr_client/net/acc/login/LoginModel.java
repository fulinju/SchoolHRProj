package sl.hr_client.net.acc.login;

import android.content.Context;

import com.android.volley.AuthFailureError;
import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.Response;
import com.android.volley.RetryPolicy;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;

import java.util.HashMap;
import java.util.Map;

import rx.Observable;
import rx.Subscriber;
import sl.base.utils.UtilsLog;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.net.ResponseUtils;
import sl.hr_client.utils.net.XStringRequest;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by Administrator on 2017/4/25.
 */

public class LoginModel {

    public Observable<String> login(final Context context, final String uLoginStr, final String uPassword, final String uClientKey) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.loginUrl;

                VolleyUtils.requestQueue = Volley.newRequestQueue(context);
                XStringRequest sr = new XStringRequest(VolleyUtils.VOLLEY_POST, targetUrl, new Response.Listener<String>() {
                    @Override
                    public void onResponse(String s) {
                        subscriber.onNext(s);
                        subscriber.onCompleted();
                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError volleyError) {
                        subscriber.onError(new Exception(ResponseUtils.responseOperate(volleyError)));
                    }
                }) {
                    @Override
                    protected Map<String, String> getParams() throws AuthFailureError {
                        Map<String, String> map = new HashMap<String, String>();
                        map.put("uLoginStr", uLoginStr);
                        map.put("uPassword", uPassword);
                        map.put("uClientKey", uClientKey);
                        return map;
                    }

//                    @Override
//                    public RetryPolicy getRetryPolicy() {
//                        RetryPolicy retryPolicy = new DefaultRetryPolicy(ConstantData.requestTimeOut, ConstantData.requestRetryTimes, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT);
//                        return retryPolicy;
//                    }
                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

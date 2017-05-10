package sl.hr_client.net.acc.forgetpwd;

import android.content.Context;

import com.android.volley.AuthFailureError;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;

import java.util.HashMap;
import java.util.Map;

import rx.Observable;
import rx.Subscriber;
import sl.hr_client.R;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.utils.net.ResponseUtils;
import sl.hr_client.utils.net.VolleyUtils;
import sl.hr_client.utils.net.XStringRequest;

/**
 * Created by Administrator on 2017/5/2.
 */

public class ForgetPwdModel {

    public Observable<String> forgetPwd(final Context context,
                                        final String uMaiBox, final String uLoginStr,
                                        final String uClientKey) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.forgetPwdUrl;

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
                        map.put("uMaiBox", uMaiBox);
                        map.put("uLoginStr", uLoginStr);
                        map.put("uClientKey", uClientKey);
                        return map;
                    }
                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

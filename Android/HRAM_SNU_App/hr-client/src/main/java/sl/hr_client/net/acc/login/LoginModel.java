package sl.hr_client.net.acc.login;

import android.content.Context;

import com.android.volley.AuthFailureError;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import java.util.HashMap;
import java.util.Map;

import rx.Observable;
import rx.Subscriber;
import sl.hr_client.utils.net.SLStringRequest;
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
                SLStringRequest sr = new SLStringRequest(VolleyUtils.VOLLEY_POST, targetUrl, new Response.Listener<String>() {
                    @Override
                    public void onResponse(String s) {
                        subscriber.onNext(s);
                        subscriber.onCompleted();
                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError volleyError) {

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
                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

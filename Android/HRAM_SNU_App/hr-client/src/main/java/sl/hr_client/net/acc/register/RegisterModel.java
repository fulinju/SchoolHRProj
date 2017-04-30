package sl.hr_client.net.acc.register;

import android.content.Context;

import com.android.volley.AuthFailureError;
import com.android.volley.Response;
import com.android.volley.VolleyError;
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

public class RegisterModel {

    public Observable<String> registerByMail(final Context context, final String mailbox) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.registerByMailUrl;

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
                        map.put("mailbox", mailbox);
                        return map;
                    }
                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

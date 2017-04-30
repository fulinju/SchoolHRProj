package sl.hr_client.net.member.detail;

import android.content.Context;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;

import rx.Observable;
import rx.Subscriber;
import sl.hr_client.utils.net.SLStringRequest;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by Administrator on 2017/4/24.
 */

public class MemberDetailModel {

    public Observable<String> getMemberDetail(final Context context, final String memberID) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.memberDetailUrl + "?memberID=" + memberID;

                VolleyUtils.requestQueue = Volley.newRequestQueue(context);
                SLStringRequest sr = new SLStringRequest(VolleyUtils.VOLLEY_GET, targetUrl, new Response.Listener<String>() {
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

                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

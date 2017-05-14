package sl.hr_client.net.link;

import android.content.Context;

import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;

import rx.Observable;
import rx.Subscriber;
import sl.hr_client.utils.net.ResponseUtils;
import sl.hr_client.utils.net.XStringRequest;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by Administrator on 2017/4/24.
 */

public class LinkModel {

    public Observable<String> getLinks(final Context context, final int pageIndex, final int pageSize) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.linksUrl + "?pageIndex=" + pageIndex + "&pageSize="+pageSize;

                VolleyUtils.requestQueue = Volley.newRequestQueue(context);
                XStringRequest sr = new XStringRequest(VolleyUtils.VOLLEY_GET, targetUrl, new Response.Listener<String>() {
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

                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

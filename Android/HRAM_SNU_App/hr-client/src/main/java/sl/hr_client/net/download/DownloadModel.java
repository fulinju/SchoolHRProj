package sl.hr_client.net.download;

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
 * Created by Administrator on 2017/4/12.
 */

public class DownloadModel {

    public Observable<String> getDownloads(final Context context, final int pageIndex, final int pageSize, final String dmTypeID) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.downloadsUrl + "?pageIndex=" + pageIndex + "&pageSize=" + pageSize;
                if (dmTypeID != null) {
                    targetUrl = targetUrl + "$dmTypeID=" + dmTypeID;
                }

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


    public Observable<String> updateDownloadNum(final Context context,final String downloadID) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.updateDownloadNumUrl + "?downloadID=" + downloadID;

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

                    }
                }) {
                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

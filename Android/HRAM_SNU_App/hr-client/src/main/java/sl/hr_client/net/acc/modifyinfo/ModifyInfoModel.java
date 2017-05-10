package sl.hr_client.net.acc.modifyinfo;

import android.content.Context;

import com.android.volley.AuthFailureError;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;

import java.util.HashMap;
import java.util.Map;

import rx.Observable;
import rx.Subscriber;
import sl.base.utils.UtilsLog;
import sl.hr_client.R;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.utils.net.ResponseUtils;
import sl.hr_client.utils.net.VolleyUtils;
import sl.hr_client.utils.net.XStringRequest;

/**
 * Created by Administrator on 2017/5/2.
 */

public class ModifyInfoModel {

    public Observable<String> modifyUserInfo(final Context context, final String uID,
                                             final String uMaiBox, final String uPhone,
                                             final String uUserName, final String uClientKey) {
        return Observable.create(new Observable.OnSubscribe<String>() {
            @Override
            public void call(final Subscriber<? super String> subscriber) {
                String targetUrl = VolleyUtils.modifyUserInfoUrl;

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
                    public Map<String, String> getHeaders() throws AuthFailureError {
                        HashMap<String, String> headers = new HashMap<String, String>();
                        UserBean user = DataUtils.getNowUserByUID();
                        String uToken = user.getUToken() == null ? context.getString(R.string.null_value) : user.getUToken();
                        String uID = user.getUID() == null ? context.getString(R.string.null_value) : user.getUID();
                        headers.put("uToken", uToken);
                        headers.put("uID", uID);
                        return headers;
                    }

                    @Override
                    protected Map<String, String> getParams() throws AuthFailureError {
                        Map<String, String> map = new HashMap<String, String>();
                        map.put("uID", uID);
                        map.put("uMaiBox", uMaiBox);
                        map.put("uPhone", uPhone);
                        map.put("uUserName", uUserName);
                        map.put("uClientKey", uClientKey);
                        return map;
                    }
                };
                VolleyUtils.requestQueue.add(sr);
            }
        });
    }
}

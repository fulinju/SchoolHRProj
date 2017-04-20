package sl.hr_client.data.parse;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

import sl.hr_client.data.bean.ADImgBean;
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.data.bean.list.DownloadListBean;
import sl.hr_client.data.bean.list.NewsListBean;

/**
 * Created by Administrator on 2017/4/6.
 */
public class GsonUtils {
    private static Gson gsonInstance = new Gson();

    public static Gson getInstance() {
        return gsonInstance;
    }

    public static NewsListBean parseNewsList(String newsListStr){
        NewsListBean news = null;
        news = getInstance().fromJson(newsListStr, NewsListBean.class);
        return news;
    }

    public static DownloadListBean parseDownloadList(String downloadListStr){
        DownloadListBean downloads = null;
        downloads = getInstance().fromJson(downloadListStr, DownloadListBean.class);
        return downloads;
    }

    public static NewsBean parseNews(String newsStr){
        NewsBean news = null;
        news = getInstance().fromJson(newsStr, NewsBean.class);
        return news;
    }

    public static String unParseAdImgList(List<ADImgBean> adList){
        return getInstance().toJson(adList);
    }

    public static List<ADImgBean> parseAdImgList(String adListStr){
        List<ADImgBean> ads = null;
        ads = jsonToArrayList(adListStr,ADImgBean.class);
        return ads;
    }

    /**
     * 解析纯数组
     * @param json
     * @param clazz
     * @param <T>
     * @return
     */
    public static <T> ArrayList<T> jsonToArrayList(String json, Class<T> clazz)
    {
        Type type = new TypeToken<ArrayList<JsonObject>>()
        {}.getType();
        ArrayList<JsonObject> jsonObjects = new Gson().fromJson(json, type);

        ArrayList<T> arrayList = new ArrayList<>();
        for (JsonObject jsonObject : jsonObjects)
        {
            arrayList.add(new Gson().fromJson(jsonObject, clazz));
        }
        return arrayList;
    }



}

package sl.hr_client.data.bean.list;

import org.greenrobot.greendao.annotation.Entity;
import org.greenrobot.greendao.annotation.Id;
import org.greenrobot.greendao.annotation.Transient;

import java.util.List;
import org.greenrobot.greendao.annotation.Generated;

import sl.hr_client.data.bean.NewsBean;

/**
 * Created by Administrator on 2017/4/6.
 */

public class NewsListBean {
    private List<NewsBean> resultList;

    public List<NewsBean> getResultList() {
        return resultList;
    }

    public void setResultList(List<NewsBean> resultList) {
        this.resultList = resultList;
    }
}

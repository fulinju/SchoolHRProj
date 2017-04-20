package sl.hr_client.data.bean.list;

import java.util.List;

import sl.hr_client.data.bean.DownloadBean;
import sl.hr_client.data.bean.NewsBean;

/**
 * Created by Administrator on 2017/4/12.
 */

public class DownloadListBean {
    private List<DownloadBean> resultList;

    public List<DownloadBean> getResultList() {
        return resultList;
    }

    public void setResultList(List<DownloadBean> resultList) {
        this.resultList = resultList;
    }
}

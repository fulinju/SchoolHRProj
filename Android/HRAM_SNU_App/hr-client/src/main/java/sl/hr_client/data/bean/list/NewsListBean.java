package sl.hr_client.data.bean.list;



import java.util.List;


import sl.hr_client.data.bean.NewsBean;

/**
 * Created by Administrator on 2017/4/6.
 */

public class NewsListBean {
    private boolean isLastPage;

    public boolean isLastPage() {
        return isLastPage;
    }

    public void setLastPage(boolean lastPage) {
        isLastPage = lastPage;
    }

    private List<NewsBean> resultList;

    public List<NewsBean> getResultList() {
        return resultList;
    }

    public void setResultList(List<NewsBean> resultList) {
        this.resultList = resultList;
    }
}

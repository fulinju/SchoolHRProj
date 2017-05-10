package sl.hr_client.data.bean.list;

import java.util.List;

import sl.hr_client.data.bean.MemberBean;

/**
 * Created by Administrator on 2017/4/24.
 */

public class MemberListBean {
    private boolean isLastPage;

    public boolean isLastPage() {
        return isLastPage;
    }

    public void setLastPage(boolean lastPage) {
        isLastPage = lastPage;
    }

    private List<MemberBean> resultList;

    public List<MemberBean> getResultList() {
        return resultList;
    }

    public void setResultList(List<MemberBean> resultList) {
        this.resultList = resultList;
    }
}

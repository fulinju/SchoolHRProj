package sl.hr_client.net.member;

/**
 * Created by Administrator on 2017/4/24.
 */

public interface MemberView {
    void updateMembersView(String str);

    void showLoading();

    void showLoadingFailed();

    void hideLoading();

    void showError(String msg);
}

package sl.hr_client.net.member.detail;

/**
 * Created by Administrator on 2017/4/24.
 */

public interface MemberDetailView {
    void updateMemberDetailView(String str);

    void showLoading();

    void showLoadingFailed();

    void hideLoading();

    void showError(String msg);
}

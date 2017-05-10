package sl.hr_client.data;

import android.content.Context;

import org.greenrobot.greendao.AbstractDao;

import java.util.List;

import sl.base.utils.UtilsPreference;
import sl.hr_client.base.BaseApplication;
import sl.hr_client.data.bean.DownloadBean;
import sl.hr_client.data.bean.LinkBean;
import sl.hr_client.data.bean.MemberBean;
import sl.hr_client.data.bean.NewsBean;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.data.dao.DaoSession;
import sl.hr_client.data.dao.DownloadBeanDao;
import sl.hr_client.data.dao.LinkBeanDao;
import sl.hr_client.data.dao.MemberBeanDao;
import sl.hr_client.data.dao.NewsBeanDao;
import sl.hr_client.data.dao.UserBeanDao;
import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by Administrator on 2017/5/2.
 */

public class DataUtils {
    /**
     * 获取当前用户信息
     * 根据UID
     *
     * @return
     */
    public static UserBean getNowUserByUID() {
        String uID = UtilsPreference.getString(ConstantData.FLAG_NOW_USER_ID, null);
        if (uID == null) {
            return null;
        }
        UserBeanDao mUserDao = BaseApplication.getInstances().getDaoSession().getUserBeanDao();
        UserBean findUser = mUserDao.queryBuilder().where(UserBeanDao.Properties.UID.eq(uID)).build().unique(); //根据UID查找
        return findUser;
    }

    /**
     * 通过删除用户
     *
     * @param id
     */
    public static void deleteUserByID(String id) {
        UserBeanDao mUserDao = BaseApplication.getInstances().getDaoSession().getUserBeanDao();
        mUserDao.deleteByKey(id);
    }


    /**
     * 添加用户
     *
     * @param user
     */
    public static void addUser(UserBean user) {
        UserBeanDao mUserDao = BaseApplication.getInstances().getDaoSession().getUserBeanDao();
        mUserDao.insert(user);
    }

    /**
     * 根据UID查找
     *
     * @param uID
     */
    public static UserBean getUserByUID(String uID) {
        UserBeanDao mUserDao = BaseApplication.getInstances().getDaoSession().getUserBeanDao();
        return mUserDao.queryBuilder().where(UserBeanDao.Properties.UID.eq(uID)).build().unique(); //根据UID查找
    }

    /**
     * 更新用户
     *
     * @param user
     */
    public static void updateUser(UserBean user) {
        UserBeanDao mUserDao = BaseApplication.getInstances().getDaoSession().getUserBeanDao();
        mUserDao.update(user);
    }

    /**
     * 缓存新闻
     *
     * @param list
     */
    public static void addNews(final List<NewsBean> list) {
        if (list == null || list.isEmpty()) {
            return;
        }
        final NewsBeanDao mNewsDao = BaseApplication.getInstances().getDaoSession().getNewsBeanDao();
        mNewsDao.getSession().runInTx(new Runnable() {
            @Override
            public void run() {
                for (int i = 0; i < list.size(); i++) {
                    NewsBean news = list.get(i);
                    mNewsDao.insertOrReplace(news);
                }
            }
        });
    }

    /**
     * 获取新闻的缓存
     *
     * @return
     */
    public static List<NewsBean> getAllNewsCache() {
        NewsBeanDao mNewsDao = BaseApplication.getInstances().getDaoSession().getNewsBeanDao();
        return mNewsDao.loadAll();
    }

    /**
     * 添加下载缓存
     * @param list
     */
    public static void addDownloads(final List<DownloadBean> list) {
        if (list == null || list.isEmpty()) {
            return;
        }
        final DownloadBeanDao mLinkDao = BaseApplication.getInstances().getDaoSession().getDownloadBeanDao();
        mLinkDao.getSession().runInTx(new Runnable() {
            @Override
            public void run() {
                for (int i = 0; i < list.size(); i++) {
                    DownloadBean link = list.get(i);
                    mLinkDao.insertOrReplace(link);
                }
            }
        });
    }

    /**
     * 获取下载缓存
     * @return
     */
    public static List<DownloadBean> getAllDownloadsCache() {
        DownloadBeanDao mDownloadDao = BaseApplication.getInstances().getDaoSession().getDownloadBeanDao();
        return mDownloadDao.loadAll();
    }


    /**
     * t添加友情链接
     *
     * @param list
     */
    public static void addLinks(final List<LinkBean> list) {
        if (list == null || list.isEmpty()) {
            return;
        }
        final LinkBeanDao mLinkDao = BaseApplication.getInstances().getDaoSession().getLinkBeanDao();
        mLinkDao.getSession().runInTx(new Runnable() {
            @Override
            public void run() {
                for (int i = 0; i < list.size(); i++) {
                    LinkBean link = list.get(i);
                    mLinkDao.insertOrReplace(link);
                }
            }
        });
    }

    /**
     * 获取缓存的友情链接
     *
     * @return
     */
    public static List<LinkBean> getAllLinksCache() {
        LinkBeanDao mLinkDao = BaseApplication.getInstances().getDaoSession().getLinkBeanDao();
        return mLinkDao.loadAll();
    }

    /**
     * 添加会员
     *
     * @param list
     */
    public static void addMembers(final List<MemberBean> list) {
        if (list == null || list.isEmpty()) {
            return;
        }
        final MemberBeanDao mMemberDao = BaseApplication.getInstances().getDaoSession().getMemberBeanDao();
        mMemberDao.getSession().runInTx(new Runnable() {
            @Override
            public void run() {
                for (int i = 0; i < list.size(); i++) {
                    MemberBean link = list.get(i);
                    mMemberDao.insertOrReplace(link);
                }
            }
        });
    }

    /**
     * 获取会员缓存
     *
     * @return
     */
    public static List<MemberBean> getAllMembersCache() {
        MemberBeanDao mMemberDao = BaseApplication.getInstances().getDaoSession().getMemberBeanDao();
        return mMemberDao.loadAll();
    }

}

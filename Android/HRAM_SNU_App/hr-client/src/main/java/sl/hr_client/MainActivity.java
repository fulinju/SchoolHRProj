package sl.hr_client;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;

import com.android.volley.toolbox.NetworkImageView;
import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;

import sl.hr_client.utils.net.VolleyUtils;

public class MainActivity extends AppCompatActivity {
    private ImageView ivTest;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_volley);

        ivTest = (ImageView) findViewById(R.id.iv_test);

        Glide.with(this)
                .load("https://cherry-cafeteria.com/CDOrderWebSys/file/1703/24111823_CreamChocolate.jpg")
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(ivTest);
    }
}

package se.jockeg.OrienteeringTvResults.LayoutService.integrationTests;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.Matchers.hasSize;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.After;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import se.jockeg.OrienteeringTvResults.LayoutService.LayoutServiceApplication;
import se.jockeg.OrienteeringTvResults.LayoutService.entities.Layout;
import se.jockeg.OrienteeringTvResults.LayoutService.repositories.LayoutRepository;

@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT, classes = LayoutServiceApplication.class)
@AutoConfigureMockMvc
@AutoConfigureTestDatabase
public class LayoutRestRontrollerIntegrationTests {

    @Autowired
    private MockMvc mvc;

    @Autowired
    private LayoutRepository repository;

    @After
    public void resetDb() {
        repository.deleteAll();
    }

    @Test
    public void testGetLayouts_layoutsExists_returnJsonArrayOfLayouts()
            throws Exception {

        var layout1 = new Layout("TV1");
        repository.save(layout1);
        var layout2 = new Layout("TV2");
        repository.save(layout2);

        mvc.perform(get("/layouts")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(content()
                        .contentTypeCompatibleWith(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$", hasSize(2)))
                .andExpect(jsonPath("$[0].Name", is("TV1")))
                .andExpect(jsonPath("$[1].Name", is("TV2")));
    }

    @Test
    public void testGetLayouts_noLayoutsExists_returnEmptyJsonArray()
            throws Exception {

        mvc.perform(get("/layouts")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$", hasSize(0)));
    }

    @Test
    public void testGetLayout_layoutExists_returnJsonLayout()
            throws Exception {

        var layout = new Layout("TV5");

        repository.save(layout);

        mvc.perform(get("/layouts/" + layout.getName())
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.Name", is(layout.getName())));
    }

    @Test
    public void testGetLayout_layoutDontExists_returnNotFound()
            throws Exception {

        mvc.perform(get("/layouts/TV5")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isNotFound());
    }
}

package se.jockeg.OrienteeringTvResults.LayoutService.unitTests.restControllers;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import se.jockeg.OrienteeringTvResults.LayoutService.entities.Layout;
import se.jockeg.OrienteeringTvResults.LayoutService.exceptions.LayoutNotFoundException;
import se.jockeg.OrienteeringTvResults.LayoutService.restControllers.LayoutRestController;
import se.jockeg.OrienteeringTvResults.LayoutService.services.LayoutService;

import java.util.Arrays;

import static org.hamcrest.Matchers.hasSize;
import static org.hamcrest.Matchers.is;
import static org.mockito.BDDMockito.given;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@RunWith(SpringRunner.class)
@WebMvcTest(LayoutRestController.class)
public class LayoutRestControllerTests {

    @Autowired
    private MockMvc mvc;

    @MockBean
    private LayoutService service;

    @Test
    public void testGetLayouts_layoutsExists_returnJsonArrayOfLayouts()
            throws Exception {

        var layout1 = new Layout("TV5");
        var layout2 = new Layout("TV6");
        var allLayouts = Arrays.asList(layout1, layout2);

        given(service.getLayouts())
                .willReturn(allLayouts);

        mvc.perform(get("/layouts")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$", hasSize(2)))
                .andExpect(jsonPath("$[0].Name", is(layout1.getName())))
                .andExpect(jsonPath("$[1].Name", is(layout2.getName())));
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

        given(service.getLayout(layout.getName())).willReturn(layout);

        mvc.perform(get("/layouts/" + layout.getName())
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.Name", is(layout.getName())));
    }

    @Test
    public void testGetLayout_layoutDontExists_returnNotFound()
            throws Exception {

        var layout = new Layout("TV5");

        given(service.getLayout(layout.getName())).willThrow(new LayoutNotFoundException());

        mvc.perform(get("/layouts/" + layout.getName())
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isNotFound());
    }
}

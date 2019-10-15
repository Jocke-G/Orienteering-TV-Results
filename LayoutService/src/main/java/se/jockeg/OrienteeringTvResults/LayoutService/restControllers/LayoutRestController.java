package se.jockeg.OrienteeringTvResults.LayoutService.restControllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import se.jockeg.OrienteeringTvResults.LayoutService.entities.*;
import se.jockeg.OrienteeringTvResults.LayoutService.exceptions.LayoutNotFoundException;
import se.jockeg.OrienteeringTvResults.LayoutService.services.*;

@RestController
@CrossOrigin(value = "*")
public class LayoutRestController {

    @Autowired
    private LayoutService layoutService;

    @RequestMapping(value = "/layouts", method = RequestMethod.GET)
    public Iterable<Layout> getLayouts() {
        return layoutService.getLayouts();
    }

    @RequestMapping(value = "/layouts/{name}", method = RequestMethod.GET)
    public Layout getLayout(@PathVariable(value="name") String name) throws LayoutNotFoundException {
        var layout = layoutService.getLayout(name);
        return layout;
    }
}

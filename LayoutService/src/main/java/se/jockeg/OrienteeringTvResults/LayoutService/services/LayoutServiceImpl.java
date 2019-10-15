package se.jockeg.OrienteeringTvResults.LayoutService.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import se.jockeg.OrienteeringTvResults.LayoutService.entities.Layout;
import se.jockeg.OrienteeringTvResults.LayoutService.exceptions.LayoutNotFoundException;
import se.jockeg.OrienteeringTvResults.LayoutService.repositories.LayoutRepository;

@Service
public class LayoutServiceImpl implements LayoutService {

    @Autowired
    private LayoutRepository layoutRepository;

    @Override
    public Iterable<Layout> getLayouts() {
        return layoutRepository.findAll();
    }

    @Override
    public Layout getLayout(String name) throws LayoutNotFoundException {
        var layout = layoutRepository.findByName(name);
        if(layout == null) {
            throw new LayoutNotFoundException();
        }
        return layoutRepository.findByName(name);
    }
}

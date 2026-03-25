import { createBrowserRouter } from "react-router-dom";
import MainPage from "./pages/MainPage";
import EventPage from "./pages/EventPage";
import ModalContextProvider from "./store/ModalContext";
import FilterContextProvider from "./store/FilterContext";

const router = createBrowserRouter([
    {
        path: "/",
        element: <FilterContextProvider><ModalContextProvider><MainPage/></ModalContextProvider></FilterContextProvider>,
        children: [
        {
            index: true,
            element: <div>
                <p>PLEASE NAVIGATE TO EVENT PAGE</p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce nisl felis, euismod tristique bibendum nec, mattis at erat. Aliquam eu volutpat orci. Ut non finibus nibh, id mollis lacus. Proin eget velit id eros ultricies aliquet. Vestibulum porttitor, nunc et mattis dictum, velit metus sollicitudin ligula, id molestie ante urna quis magna. Cras auctor dolor dolor, sit amet dapibus erat blandit sit amet. Integer eleifend urna ac dictum elementum.

                Cras quis enim massa. Proin dapibus posuere lacinia. Proin in urna sit amet lectus cursus tempor in at arcu. Sed placerat feugiat ultrices. Cras vel eros ultrices, suscipit diam vitae, viverra tortor. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed nunc orci, eleifend vitae ligula a, aliquet congue velit. Nullam bibendum erat ante, at consectetur nisl condimentum nec. Sed sodales quam non magna egestas auctor. In vel erat in mi ultrices porta sit amet at lorem. Maecenas nec semper leo, ut mattis mi.

                Praesent accumsan velit ut ligula feugiat ornare. Etiam ut suscipit lectus. Quisque ullamcorper mi orci, sit amet mattis dui consectetur efficitur. Donec at sodales felis, vel porta mi. Vestibulum feugiat diam sit amet eleifend tristique. Maecenas magna tortor, elementum et diam sit amet, auctor iaculis dolor. Pellentesque porta tempus dolor, a sollicitudin nunc rutrum nec. Suspendisse ut convallis magna. In eleifend purus ac risus molestie, vel tincidunt purus consequat. Vivamus nunc tortor, scelerisque in mauris eget, dictum consectetur enim. Pellentesque est mi, faucibus ac egestas id, venenatis non magna. Aliquam pretium non metus at hendrerit. Integer lacinia odio mi, vitae placerat ipsum congue quis. Aenean eleifend eu ipsum non aliquet.

                Donec eget hendrerit leo. Suspendisse lectus ipsum, vestibulum in arcu nec, luctus lacinia quam. Integer eu nibh nisi. Fusce interdum, dui sed rutrum vulputate, felis nunc tristique nunc, id varius dui nunc sit amet turpis. Nulla maximus velit eget laoreet laoreet. Morbi in egestas orci. Nam sit amet mattis velit. Donec dignissim, mauris nec vestibulum faucibus, mauris sapien auctor nibh, et auctor magna enim ut metus. Maecenas venenatis id lacus et maximus. Maecenas eget sem non sem efficitur semper at at lectus. Fusce elementum, ante egestas faucibus venenatis, nunc elit ultrices erat, vel malesuada diam nisl vel neque. Phasellus tempus sit amet arcu a tempus. Vestibulum condimentum volutpat semper. Nam et aliquet orci, nec euismod nibh.

                Donec nec urna at odio aliquam pharetra quis id dui. Quisque nec lorem pellentesque, iaculis tellus vitae, tempus magna. Ut porta, mauris vestibulum malesuada blandit, velit ipsum tincidunt diam, nec condimentum orci orci quis nunc. Donec ut libero dapibus ante semper malesuada. Mauris nunc quam, condimentum eu ligula eu, porta mattis dolor. Curabitur interdum ornare nulla, sed interdum orci placerat a. Quisque eget ex orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Fusce lacinia blandit dui, eu facilisis nunc tristique at. Duis nec tortor nec nisl commodo viverra. Duis fringilla elit quis tellus pellentesque, nec semper nunc consequat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Morbi auctor tristique dolor, eu venenatis ex mollis eu.
            </div>
        },
        {
            path: "events",
            element: <EventPage/>
        },
        {
            path: "competitions",
            element: <div>
                <p>PLEASE NAVIGATE TO EVENT PAGE</p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce nisl felis, euismod tristique bibendum nec, mattis at erat. Aliquam eu volutpat orci. Ut non finibus nibh, id mollis lacus. Proin eget velit id eros ultricies aliquet. Vestibulum porttitor, nunc et mattis dictum, velit metus sollicitudin ligula, id molestie ante urna quis magna. Cras auctor dolor dolor, sit amet dapibus erat blandit sit amet. Integer eleifend urna ac dictum elementum.

                Cras quis enim massa. Proin dapibus posuere lacinia. Proin in urna sit amet lectus cursus tempor in at arcu. Sed placerat feugiat ultrices. Cras vel eros ultrices, suscipit diam vitae, viverra tortor. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed nunc orci, eleifend vitae ligula a, aliquet congue velit. Nullam bibendum erat ante, at consectetur nisl condimentum nec. Sed sodales quam non magna egestas auctor. In vel erat in mi ultrices porta sit amet at lorem. Maecenas nec semper leo, ut mattis mi.

                Praesent accumsan velit ut ligula feugiat ornare. Etiam ut suscipit lectus. Quisque ullamcorper mi orci, sit amet mattis dui consectetur efficitur. Donec at sodales felis, vel porta mi. Vestibulum feugiat diam sit amet eleifend tristique. Maecenas magna tortor, elementum et diam sit amet, auctor iaculis dolor. Pellentesque porta tempus dolor, a sollicitudin nunc rutrum nec. Suspendisse ut convallis magna. In eleifend purus ac risus molestie, vel tincidunt purus consequat. Vivamus nunc tortor, scelerisque in mauris eget, dictum consectetur enim. Pellentesque est mi, faucibus ac egestas id, venenatis non magna. Aliquam pretium non metus at hendrerit. Integer lacinia odio mi, vitae placerat ipsum congue quis. Aenean eleifend eu ipsum non aliquet.

                Donec eget hendrerit leo. Suspendisse lectus ipsum, vestibulum in arcu nec, luctus lacinia quam. Integer eu nibh nisi. Fusce interdum, dui sed rutrum vulputate, felis nunc tristique nunc, id varius dui nunc sit amet turpis. Nulla maximus velit eget laoreet laoreet. Morbi in egestas orci. Nam sit amet mattis velit. Donec dignissim, mauris nec vestibulum faucibus, mauris sapien auctor nibh, et auctor magna enim ut metus. Maecenas venenatis id lacus et maximus. Maecenas eget sem non sem efficitur semper at at lectus. Fusce elementum, ante egestas faucibus venenatis, nunc elit ultrices erat, vel malesuada diam nisl vel neque. Phasellus tempus sit amet arcu a tempus. Vestibulum condimentum volutpat semper. Nam et aliquet orci, nec euismod nibh.

                Donec nec urna at odio aliquam pharetra quis id dui. Quisque nec lorem pellentesque, iaculis tellus vitae, tempus magna. Ut porta, mauris vestibulum malesuada blandit, velit ipsum tincidunt diam, nec condimentum orci orci quis nunc. Donec ut libero dapibus ante semper malesuada. Mauris nunc quam, condimentum eu ligula eu, porta mattis dolor. Curabitur interdum ornare nulla, sed interdum orci placerat a. Quisque eget ex orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Fusce lacinia blandit dui, eu facilisis nunc tristique at. Duis nec tortor nec nisl commodo viverra. Duis fringilla elit quis tellus pellentesque, nec semper nunc consequat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Morbi auctor tristique dolor, eu venenatis ex mollis eu.
            </div>
        },
        {
            path: "about-us",
            element: <div>
                <p>PLEASE NAVIGATE TO EVENT PAGE</p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce nisl felis, euismod tristique bibendum nec, mattis at erat. Aliquam eu volutpat orci. Ut non finibus nibh, id mollis lacus. Proin eget velit id eros ultricies aliquet. Vestibulum porttitor, nunc et mattis dictum, velit metus sollicitudin ligula, id molestie ante urna quis magna. Cras auctor dolor dolor, sit amet dapibus erat blandit sit amet. Integer eleifend urna ac dictum elementum.

                Cras quis enim massa. Proin dapibus posuere lacinia. Proin in urna sit amet lectus cursus tempor in at arcu. Sed placerat feugiat ultrices. Cras vel eros ultrices, suscipit diam vitae, viverra tortor. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed nunc orci, eleifend vitae ligula a, aliquet congue velit. Nullam bibendum erat ante, at consectetur nisl condimentum nec. Sed sodales quam non magna egestas auctor. In vel erat in mi ultrices porta sit amet at lorem. Maecenas nec semper leo, ut mattis mi.

                Praesent accumsan velit ut ligula feugiat ornare. Etiam ut suscipit lectus. Quisque ullamcorper mi orci, sit amet mattis dui consectetur efficitur. Donec at sodales felis, vel porta mi. Vestibulum feugiat diam sit amet eleifend tristique. Maecenas magna tortor, elementum et diam sit amet, auctor iaculis dolor. Pellentesque porta tempus dolor, a sollicitudin nunc rutrum nec. Suspendisse ut convallis magna. In eleifend purus ac risus molestie, vel tincidunt purus consequat. Vivamus nunc tortor, scelerisque in mauris eget, dictum consectetur enim. Pellentesque est mi, faucibus ac egestas id, venenatis non magna. Aliquam pretium non metus at hendrerit. Integer lacinia odio mi, vitae placerat ipsum congue quis. Aenean eleifend eu ipsum non aliquet.

                Donec eget hendrerit leo. Suspendisse lectus ipsum, vestibulum in arcu nec, luctus lacinia quam. Integer eu nibh nisi. Fusce interdum, dui sed rutrum vulputate, felis nunc tristique nunc, id varius dui nunc sit amet turpis. Nulla maximus velit eget laoreet laoreet. Morbi in egestas orci. Nam sit amet mattis velit. Donec dignissim, mauris nec vestibulum faucibus, mauris sapien auctor nibh, et auctor magna enim ut metus. Maecenas venenatis id lacus et maximus. Maecenas eget sem non sem efficitur semper at at lectus. Fusce elementum, ante egestas faucibus venenatis, nunc elit ultrices erat, vel malesuada diam nisl vel neque. Phasellus tempus sit amet arcu a tempus. Vestibulum condimentum volutpat semper. Nam et aliquet orci, nec euismod nibh.

                Donec nec urna at odio aliquam pharetra quis id dui. Quisque nec lorem pellentesque, iaculis tellus vitae, tempus magna. Ut porta, mauris vestibulum malesuada blandit, velit ipsum tincidunt diam, nec condimentum orci orci quis nunc. Donec ut libero dapibus ante semper malesuada. Mauris nunc quam, condimentum eu ligula eu, porta mattis dolor. Curabitur interdum ornare nulla, sed interdum orci placerat a. Quisque eget ex orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Fusce lacinia blandit dui, eu facilisis nunc tristique at. Duis nec tortor nec nisl commodo viverra. Duis fringilla elit quis tellus pellentesque, nec semper nunc consequat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Morbi auctor tristique dolor, eu venenatis ex mollis eu.
            </div>
        }
    ]
    }
])

export default router;
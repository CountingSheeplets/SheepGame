const char = new PIXI.Application({
    width: 400, height: 300, backgroundColor: 0x1099bb, resolution: window.devicePixelRatio || 1,
});
document.body.appendChild(char.view);

const container = new PIXI.Container();

char.stage.addChild(container);

// Create a new texture
const texture = PIXI.Texture.from('examples/images/cat.png');

// Create a 5x5 grid of bunnies
for (let i = 0; i < 3; i++) {
    const bunny = new PIXI.Sprite(texture);
    bunny.anchor.set(0.5);
    bunny.x = (i % 3) * 75;
    bunny.y = Math.floor(i / 5) * 75;
    container.addChild(bunny);
}

// Move container to the center
container.x = char.screen.width / 2;
container.y = char.screen.height / 2;

// Center bunny sprite in local container coordinates
container.pivot.x = container.width / 2;
container.pivot.y = container.height / 2;

// Listen for animate update
char.ticker.add((delta) => {
    // rotate the container!
    // use delta to create frame-independent transform
    container.rotation -= 0.01 * delta;
});
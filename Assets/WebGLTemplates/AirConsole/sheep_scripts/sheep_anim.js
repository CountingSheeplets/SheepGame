"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
/**
 * How to use
 * 1. Load data.
 *
 * 2. Parse data.
 *    factory.parseDragonBonesData();
 *    factory.parseTextureAtlasData();
 *
 * 3. Build armature.
 *    armatureDisplay = factory.buildArmatureDisplay("armatureName");
 *
 * 4. Play animation.
 *    armatureDisplay.animation.play("animationName");
 *
 * 5. Add armature to stage.
 *    addChild(armatureDisplay);
 */
var SheepCharacter = /** @class */ (function (_super) {
    __extends(SheepCharacter, _super);
    function SheepCharacter() {
        var _this = _super.call(this) || this;
        _this._resources.push(
        //    "resource/mecha_1002_101d_show/mecha_1002_101d_show_ske.dbbin", 
        "sheep_resources/test5_ske.json",
        "sheep_resources/test5_tex.json",
        "sheep_resources/test5_tex.png");
        return _this;
    }
    SheepCharacter.prototype._onStart = function () {
        var factory = dragonBones.PixiFactory.factory;
        factory.parseDragonBonesData(this._pixiResources["sheep_resources/test5_ske.json"].data);
        //factory.parseDragonBonesData(this._pixiResources["resource/mecha_1002_101d_show/mecha_1002_101d_show_ske.dbbin"].data);
        factory.parseTextureAtlasData(this._pixiResources["sheep_resources/test5_tex.json"].data,
        this._pixiResources["sheep_resources/test5_tex.png"].texture);
        var armatureDisplay = factory.buildArmatureDisplay("dude", "test5");
        armatureDisplay.animation.play("juda");
        armatureDisplay.x = 0.0;
        armatureDisplay.y = 0.0;
        this.addChild(armatureDisplay);
    };
    return SheepCharacter;
}(BaseDemo));
